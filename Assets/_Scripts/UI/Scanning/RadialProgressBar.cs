using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RadialProgressBar : ScanningUI
{
    private ScannableUnit _currentUnit = null;

    [SerializeField] private Image _radialProgressBar;
    private bool _isScanning = false;

    protected override void Awake()
    {
        base.Awake();
        _radialProgressBar = GetComponent<Image>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _scanner.UnitInFocus.AddListener(OnUnitChange);
        Debug.Log("Subscribed to scanner");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _scanner.UnitInFocus.RemoveListener(OnUnitChange);
        if (_currentUnit != null) _currentUnit.ScanProgress.RemoveListener(UpdateProgressBar);
    }

    private void OnUnitChange(ScannableUnit unit)
    {
        if (_currentUnit != null) 
        {
            _currentUnit.ScanProgress.RemoveListener(UpdateProgressBar);
            Debug.Log($"No longer listening to old unit: {_currentUnit.name}");
        }
        Debug.Log($"Changing current Unit to {unit}");
        _currentUnit = unit;
        if (_currentUnit != null)
        {
            _currentUnit.ScanProgress.AddListener(UpdateProgressBar);
            Debug.Log($"Now listening to new unit: {_currentUnit.name}");
        }
    }

    protected override void OnScanChange(bool isScanning)
    {
        // TODO: out of scope. work on later.
    }

    private void UpdateProgressBar(float value)
    {
        // TODO: Lerp fillAmount for gradual fill
        _radialProgressBar.fillAmount = Mathf.Clamp01(value/100.0f);

        if (value >= 100)
        {
            OnUnitChange(null);
            _radialProgressBar.fillAmount = 0;
        }
    }
}
