using System.Collections;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class MissionItem : Item
    {
        public int MoneyLevel { get; set; }
        public abstract Transform Waypoints { get; set; }
        private Soldier _soldier;
        private SoldierWalkUtil wayBack;

        public void MoneyUpgrade()
        {
            MoneyLevel++;
        }
        
        public override void SoldierSitDown(Soldier soldier)
        {
            _soldier = soldier;
            soldier.gameObject.SetActive(false);
            MissionStart();
        }
        
        public void MissionStart()
        {
            GetComponent<Animator>().SetTrigger("Mission_Start");
        }
        
        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            GetComponent<Animator>().SetTrigger("Mission_End");
        }
        
        public void getBackDelayed()
        {
            StartCoroutine(ExecuteAfterTime(calculateDuration()));
        }
        
        public float calculateDuration()
        {
            // Duration - AnimationDuration
            float animationDuration = 9; // TODO - compute value somehow?!
            return TimeNeeded() - animationDuration;
            
        }
        
        public void MissionEnd()
        {
            Reward();
            LetSoldierMove();
        }

        private void LetSoldierMove()
        {
            _soldier.gameObject.SetActive(true);
            _soldier.anim.SetBool("isRunning",true);
            Occupied = false;
            Parent.ItemIsFree();
            wayBack = new SoldierWalkUtil(_soldier, null, () => RoutingPoint.LetSoldierMove(_soldier), RemoveWayBack, .2f,
                Waypoints.GetAllChildren().Reverse().ToArray());

        }

        public void Reward()
        {
            GameManager.INSTANCE.Gold += 500; // TODO - more specific

        }
        private void RemoveWayBack(SoldierWalkUtil util)
        {
            wayBack = null;
        }
        
        private void Update()
        {
            wayBack?.Update();
        }
    }
}