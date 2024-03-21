using UnityEngine;
using DesignPatterns;
using Player;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Rendering;

namespace Player
{
    public class Hero : Singleton<Hero>
    {
        public MonsterProperties MonsterProperties { set {MonsterProperties = value;} }

        protected override void Awake()
        {
            base.Awake();
        }

    }
}

