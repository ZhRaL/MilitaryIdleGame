using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Chair : MonoBehaviour
    {
        private float distanceSoldierGoDown = .3f;
        private bool _unlocked;
        public bool Unlocked
        {
            get { return _unlocked;}
            set
            {
                gameObject.SetActive(value);
                _unlocked = value;
            }
        }

        public bool Occupied { get; set; }
        public RoutingPoint RoutingPoint;
        public Table Table;
        private float _timeLeft;
        private Soldier _soldier;
        
        private void Update()
        {
            if(!_soldier) return;
            
           // _timeLeft -= Time.deltaTime;
           // if(_timeLeft<0) SoldierGetUp();
        }

        public void SoldierSitDown(Soldier soldier)
        {
            _soldier = soldier;
            _timeLeft = Table.getWaitingAmount();
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

            RoutingPoint.LetSoldierMove(_soldier);
            _soldier = null;
        }
    }
}