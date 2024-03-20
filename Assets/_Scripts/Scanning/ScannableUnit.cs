using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class ScannableUnit : MonoBehaviour
{
    [SerializeField] protected ScannableUnitProperties _scannableUnit;
    [SerializeField] private Observer<float> _scanProgress = new Observer<float>(0);
    [SerializeField] private bool _shouldDecrement = true; 
    private bool _isBeingScanned = false;
    private bool _isScannable = true;
    private bool _isScanComplete = false;
    private WaitForSeconds _cdWaitTime;

    public Observer<float> ScanProgress { get { return _scanProgress; } }
    public GameObject PlayableUnitGameObject { get { return _scannableUnit._scannedUnitPfb; } }

    protected void Awake()
    {
        _cdWaitTime = new WaitForSeconds(_scannableUnit._cdWaitTime);
    }

    private void OnEnable()
    {
        _scanProgress.AddListener(OnSuccessfulScan);
    }

    protected virtual void Start()
    {
        if (_shouldDecrement) StartCoroutine(DecrementScanProgress());
    }

    protected virtual void Update() { }

    private void OnDisable()
    {
        _scanProgress.RemoveListener(OnSuccessfulScan);
    }

    public void OnSuccessfulScan(float progress)
    {
        if (progress < 100f) return;
        Debug.Log($"Scan Progress is: {progress}. Called from Abstract Class ScannableUnit");
        _isScannable = false;
        _isScanComplete = true;
        _shouldDecrement = false;
    }

    public void ScanUnit()
    {
        if (_isScannable && !_isBeingScanned) StartCoroutine(IncrementScanProgress());

        IEnumerator IncrementScanProgress()
        {
            _isBeingScanned = true;
            _scanProgress.Value = Mathf.Clamp(_scanProgress.Value + _scannableUnit._scanIncrementRate, 0, 100f);
            yield return _cdWaitTime;
            Debug.Log("Calling ScanUnit() from Abstract Class ScannableUnit");
            _isBeingScanned = false;
        }
    }

    private IEnumerator DecrementScanProgress()
    {
        while (_shouldDecrement)
        {
            if (!_isBeingScanned) _scanProgress.Value = Mathf.Clamp(_scanProgress.Value - _scannableUnit._scanDecrementRate, 0, 100f);
            yield return _cdWaitTime;
        }
    }

    public void ResetUnit()
    {
        _scanProgress.Value = 0;
        _shouldDecrement = true; 
        _isBeingScanned = false;
        _isScannable = true;
        _isScanComplete = false;
    }

}
