using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoldierWalkUtil
    {
        public Soldier Soldier { get; set; }
        public Transform Target { get; set; }
        public Action Action { get; set; }
        public Action<SoldierWalkUtil> FinishedListener{ get; set; }
        Vector3 _targetDirection;
        private float distanceToTarget;


        public SoldierWalkUtil(Soldier soldier, Transform target, Action action, Action<SoldierWalkUtil> finishedListener)
        {
            Soldier = soldier;
            Target = target;
            _targetDirection = (Target.position - soldier.transform.position).normalized;
            Action = action;
            FinishedListener = finishedListener;
            distanceToTarget = .2f;
        }
        public SoldierWalkUtil(Soldier soldier, Transform target, Action action, Action<SoldierWalkUtil> finishedListener, float distanceToTarget)
        {
            Soldier = soldier;
            Target = target;
            _targetDirection = (Target.position - soldier.transform.position).normalized;
            Action = action;
            FinishedListener = finishedListener;
            this.distanceToTarget = distanceToTarget;
        }

        private void ReachedTarget()
        {
            FinishedListener.Invoke(this);
            Action.Invoke();
        }

        public void Update()
        {
            _targetDirection = (Target.position - Soldier.transform.position).normalized;
            Vector3 forward = Target.position - Soldier.transform.position;
            Quaternion neededRotation = Quaternion.LookRotation(forward);
            Soldier.transform.rotation = Quaternion.RotateTowards(Soldier.transform.rotation, neededRotation, Time.deltaTime * 200f);
            
            Soldier.transform.position += _targetDirection * Soldier.Speed * Time.deltaTime;
            
            double distance = (Soldier.transform.position - Target.position).magnitude;
            if ( distance < .2f)
            {
                Soldier.transform.position = Target.position; 
                ReachedTarget();
            }
            // schießen übers Ziel hinaus
        }
    }
}