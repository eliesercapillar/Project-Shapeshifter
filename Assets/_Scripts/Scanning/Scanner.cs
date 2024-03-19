using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using DesignPatterns;

namespace Player
{
    public class Scanner : Singleton<Scanner>
    {
        [Header("Cameras")]
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _neutralCamera;
        [SerializeField] private GameObject _aimCamera;
        private bool _areCamerasFlipped = false;

        [Header("Scan Properties")]
        // scan distance between 10 - 20 units feels good.
        [SerializeField] private float _scanDistance = 15;
        private Ray _aimRay;

        private Observer<bool> _isScanning = new Observer<bool>(false);
        private Observer<ScannableUnit> _unitInFocus = new Observer<ScannableUnit>(null);

        public Observer<bool> IsScanning { get { return _isScanning; } }
        public Observer<ScannableUnit> UnitInFocus { get { return _unitInFocus; } }

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
        }

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                if (!_areCamerasFlipped) 
                {
                    _areCamerasFlipped = true;
                    FlipFlopCameras();
                }
                if (Input.GetMouseButton(0))
                {
                    _isScanning.Value = true;
                    PerformScan();
                }
                else _isScanning.Value = false;
            }
            else if (!_neutralCamera.activeSelf)
            {
                _isScanning.Value = false;
                _areCamerasFlipped = false;
                FlipFlopCameras();
            }
        }

        private void PerformScan()
        {
            //_aimRay = _camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
            _aimRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(_aimRay, out hitInfo, _scanDistance))
            {
                if (hitInfo.collider.tag != "ScannableEnemy") return;
                
                ScannableUnit unit = hitInfo.collider.GetComponent<ScannableUnit>();
                _unitInFocus.Value = unit;
                // _isScanning.Value = true; // Out of scope. Work on later.
                unit.ScanUnit();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_aimRay);
        }

        private void FlipFlopCameras()
        {
            _neutralCamera.SetActive(!_neutralCamera.activeSelf);
            _aimCamera.SetActive(!_aimCamera.activeSelf);
        }

    }
}

