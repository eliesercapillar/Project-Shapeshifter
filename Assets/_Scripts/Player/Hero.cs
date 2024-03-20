using UnityEngine;
using DesignPatterns;
using Player;
using System.Collections.Generic;
using Cinemachine;

namespace Player
{
    public class Hero : Singleton<Hero>
    {
        //[SerializeField] private ScannerListener _scannerListener;
        private Scanner _scanner;
        private ScannableUnit _currentUnit = null;
        
        private List<GameObject> _scannedUnits;

        private bool _isTransformed = false;

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
            _scannedUnits = new();
        }

    }
}

