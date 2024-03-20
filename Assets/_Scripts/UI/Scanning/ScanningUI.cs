using Player;
using UnityEngine;

public abstract class ScanningUI: MonoBehaviour
{
    protected Scanner _scanner;

    protected virtual void Awake()
    {
        _scanner = Scanner.Instance;
    }

    protected virtual void OnEnable()
    {
        _scanner.IsScanning.AddListener(OnScanChange);
    }

    protected virtual void OnDisable()
    {
        _scanner.IsScanning.RemoveListener(OnScanChange);
    }

    protected abstract void OnScanChange(bool isScanning);
}
