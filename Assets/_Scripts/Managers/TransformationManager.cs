using UnityEngine;
using DesignPatterns;
using Player;
using System.Collections.Generic;

namespace Managers
{
    public class TransformationManager : PersistentSingleton<TransformationManager>
    {
        //[SerializeField] private ScannerListener _scannerListener;
        [SerializeField] private CameraManager _cameraManager;
        private Scanner _scanner;
        private ScannableUnit _currentUnit = null;
        
        private List<GameObject> _scannedUnits;

        private bool _isTransformed = false;

        private Hero _hero;

        // [Header("Cameras")]
        // [SerializeField] private Camera _camera;
        // [SerializeField] private GameObject _neutralCamera;
        // [SerializeField] private GameObject _aimCamera;
        // private bool _areCamerasFlipped = false;

        // [Header("Scan Properties")]
        // // scan distance between 10 - 20 units feels good.
        // [SerializeField] private float _scanDistance = 15;
        // private Ray _aimRay;

        // private Observer<bool> _isScanning = new Observer<bool>(false);
        // private Observer<ScannableUnit> _unitInFocus = new Observer<ScannableUnit>(null);

        // public Observer<bool> IsScanning { get { return _isScanning; } }
        // public Observer<ScannableUnit> UnitInFocus { get { return _unitInFocus; } }

        protected override void Awake()
        {
            base.Awake();
            _scanner = Scanner.Instance;
            _hero = Hero.Instance;
            _scannedUnits = new();
        }

        private void OnEnable()
        {
            _scanner.UnitInFocus.AddListener(OnUnitChange);
        }

        void Start()
        {

        }

        private void OnDisable()
        {
            _scanner.UnitInFocus.RemoveListener(OnUnitChange);
            if (_currentUnit != null) _currentUnit.ScanProgress.RemoveListener(CheckTransformProgress);
        }

        private void OnUnitChange(ScannableUnit unit)
        {
            if (_currentUnit != null) _currentUnit.ScanProgress.RemoveListener(CheckTransformProgress);
            _currentUnit = unit;
            if (_currentUnit != null) _currentUnit.ScanProgress.AddListener(CheckTransformProgress);
        }

        private void CheckTransformProgress(float progress)
        {
            if (progress < 100.0f) return;
            //if (!_scannedUnits.Contains(_currentUnit.PlayableUnitGameObject)) _scannedUnits.Add(_currentUnit.PlayableUnitGameObject);
            if (!_isTransformed) AttemptTransformation(_currentUnit.PlayableUnitGameObject, _currentUnit.MonsterProperties);
        }

        private void AttemptTransformation(GameObject newForm, MonsterProperties mp)
        {
            _isTransformed = true;
            _hero.gameObject.SetActive(false);
            GameObject go = Instantiate(newForm, _hero.transform.position, _hero.transform.rotation);
            _cameraManager.UpdateVCams(go);
            _hero = go.AddComponent<Hero>();
            //_hero.MonsterProperties = mp;
        }
    }
}

