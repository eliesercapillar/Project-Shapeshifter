using UnityEngine;

[CreateAssetMenu(fileName = "Usable Monster Properties", menuName = "Scriptable Objects/Usable Monster Properties")]
public class MonsterProperties : ScriptableObject
{
    public UnitType _unitType;
    public GameObject _scannedUnitPfb;

    // Scanning Properties
    public float _scanIncrementRate;
    public float _scanDecrementRate;
    public float _cdWaitTime;
}