using System.Collections;
using DesignPatterns;
using UnityEngine;

public enum UnitType
{
    Player,
    NPC,
    Monster,
    Boss_Monster
}

[CreateAssetMenu(fileName = "Scannable Unit Properties", menuName = "Scriptable Objects/Scannable Unit Properties")]
public class ScannableUnitProperties : ScriptableObject
{
    public UnitType _unitType;
    public GameObject _scannedUnitPfb;

    // Scanning Properties
    public float _scanIncrementRate;
    public float _scanDecrementRate;
    public float _cdWaitTime;
}
