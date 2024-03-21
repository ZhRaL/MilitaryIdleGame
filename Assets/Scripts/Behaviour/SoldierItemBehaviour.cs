using System;
using UnityEngine;
using Util;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class SoldierItemBehaviour
    {
        
        public Soldier Soldier { get; set; }
        public Item Item { get; set; }

        public SoldierItemBehaviour(Item item)
        {
            Item = item;
        }
        
        public void SoldierSitDown(Soldier soldier)
        {
            Soldier = soldier;
            Soldier.anim.SetBool("isRunning",false);
            Vector3 newPos = soldier.transform.position;
            // Offset for animation needs to be berücksichtigt
            // Maybe as Vector3 as a field?!
            Soldier.transform.position = newPos;
            GameObject rb = Object.Instantiate(Soldier.RadialBarPrefab, Soldier.transform);
            rb.transform.rotation = Camera.main.transform.rotation;
            rb.GetComponent<RadialBar>().Initialize(Item.TimeNeeded(),SoldierGetUp);
        }

        public void SoldierGetUp()
        {
            Item.Occupied = false;
            var transform1 = Soldier.transform;
            Vector3 newPos = transform1.position;
            // Remove the Offset
            transform1.position = newPos;

            Item.Parent.ItemIsFree();
            Item.RoutingPoint.LetSoldierMove(Soldier);
            Soldier = null;
        }
    }
}