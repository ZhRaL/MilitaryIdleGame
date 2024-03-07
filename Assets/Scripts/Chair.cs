using System;
using Interfaces;
using UnityEngine;
using Util;

namespace DefaultNamespace
{
    public class Chair : Item,IGatherable
    {
        private float distanceSoldierGoDown = .3f;
        
        public RoutingPoint RoutingPoint;
        public Table Table;
        private float _timeLeft;
        private Soldier _soldier;
        [SerializeField]
        private int _level;

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        private void Update()
        {
            if(!_soldier) return;
            
           // _timeLeft -= Time.deltaTime;
           // if(_timeLeft<0) SoldierGetUp();
        }

        public float getTimeForRound()
        {
            return 10 - Calculator.INSTANCE.getTimeReductionReward(Level);
        }

        public void SoldierSitDown(Soldier soldier)
        {
            _soldier = soldier;
            _timeLeft = getTimeForRound();
            soldier.anim.SetBool("isRunning",false);
            Vector3 newPos = soldier.transform.position;
            newPos.y -= distanceSoldierGoDown;
            soldier.transform.position = newPos;
            GameObject rb = Instantiate(soldier.RadialBarPrefab, soldier.transform);
            rb.transform.rotation = Camera.main.transform.rotation;
            rb.GetComponent<RadialBar>().Initialize(_timeLeft,SoldierGetUp);
        }

        private void SoldierGetUp()
        {
            Occupied = false;
            var transform1 = _soldier.transform;
            Vector3 newPos = transform1.position;
            newPos.y += distanceSoldierGoDown;
            transform1.position = newPos;

            Table.ItemIsFree();
            RoutingPoint.LetSoldierMove(_soldier);
            _soldier = null;
        }

        public int GetData()
        {
            return Level;
        }

        public void Upgrade()
        {
            Level++;
             logger.log("Chair was upgraded to Level "+_level);
        }
    }
}