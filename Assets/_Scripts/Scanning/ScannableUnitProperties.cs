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
    public float _scanAmount = 100.0f;
    public float _scanRate;
    public float _cdWaitTime;
}
