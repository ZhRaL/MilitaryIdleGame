using System;
using Interfaces;
using UnityEngine;
using Util;

namespace DefaultNamespace
{
    public class Chair : Item
    {
        public override void SoldierSitDown(Soldier soldier)
        {
            base.SoldierSitDown(soldier);
            soldier.transform.rotation = gameObject.transform.rotation;
        }
    }
}