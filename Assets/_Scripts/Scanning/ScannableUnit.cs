using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class ScannableUnit : MonoBehaviour
{
    [SerializeField] protected ScannableUnitProperties _scannableUnit;
    [SerializeField] private Observer<float> _scanProgress = new Observer<float>(0);
    private bool _onScanCD = false;
    private WaitForSeconds _cdWaitTime;

    public Observer<float> ScanProgress { get { return _scanProgress; } }

    protected void Awake()
    {
        _cdWaitTime = new WaitForSeconds(_scannableUnit._cdWaitTime);
        Debug.Log("Creating WaitForSeconds");
    }

    private void OnEnable()
    {
        //Debug.Log("Adding listener from Abstract Class ScannableUnit");
        _scanProgress.AddListener(OnSuccessfulScan);
    }

    private void OnDisable()
    {
        //Debug.Log("Removing listener from Abstract Class ScannableUnit");
        _scanProgress.RemoveListener(OnSuccessfulScan);
    }

    public void OnSuccessfulScan(float value)
    {
        Debug.Log($"Scan Progress is: {value}. Called from Abstract Class ScannableUnit");
        if (value < _scannableUnit._scanAmount) return;

        
    }

    public void ScanUnit()
    {
        if (!_onScanCD) StartCoroutine(IncrementScanProgress());

        IEnumerator IncrementScanProgress()
        {
            _onScanCD = true;
            _scanProgress.Value = Mathf.Clamp(_scanProgress.Value + _scannableUnit._scanRate, 0.0f, _scannableUnit._scanAmount);
            yield return _cdWaitTime;
            Debug.Log("Calling ScanUnit() from Abstract Class ScannableUnit");
            _onScanCD = false;
        }
    }

}
