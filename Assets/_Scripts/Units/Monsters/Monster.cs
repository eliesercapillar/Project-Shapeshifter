using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class Monster : ScannableUnit
{
    [SerializeField] public Observer<int> _health;

}
