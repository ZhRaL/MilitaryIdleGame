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
            Soldier.anim.SetBool("isRunning", false);
            AnimatorStateInfo stateInfo = Soldier.anim.GetCurrentAnimatorStateInfo(0);

            if (!stateInfo.IsName("Run"))
            {
                Soldier.anim.speed = 1;
            }

            GameObject rb = Object.Instantiate(Soldier.RadialBarPrefab, Soldier.transform);
            rb.transform.rotation = Camera.main.transform.rotation;

            if (Item is not Bed)
            {
                Soldier.anim.SetTrigger("SittingDownTrigger");
                Soldier.anim.ResetTrigger("SittingUpTrigger");
                rb.GetComponent<RadialBar>().Initialize(Item.TimeNeeded(), SoldierGetUp, new ActionBefore(2.217f, StartGettingUp));
                soldier.transform.rotation = Item.transform.rotation;
                soldier.transform.position += Item.transform.forward * .3f;
                return;
            }
            else
            {
                Soldier.anim.SetTrigger("LayingDownTrigger");
                Soldier.anim.ResetTrigger("LayingUpTrigger");
                rb.GetComponent<RadialBar>().Initialize(Item.TimeNeeded(), SoldierGetUp, new ActionBefore(2.217f, StartGettingUp));
                // TODO - Caluclate Duration of LayingUp Animation
                return;
            }

        }

        public void StartGettingUp()
        {

            if (Item is Chair)
                Soldier.anim.SetTrigger("SittingUpTrigger");
            else
                Soldier.anim.SetTrigger("LayingUpTrigger");
        }

        public void SoldierGetUp()
        {
            Item.Occupied = false;
            var transform1 = Soldier.transform;
            Vector3 newPos = transform1.position;
            Soldier.anim.SetBool("isRunning", true);
            // Remove the Offset
            transform1.position = newPos;

            Item.Parent.ItemIsFree();
            Item.RoutingPoint.LetSoldierMove(Soldier);
            Soldier = null;
        }
    }
}