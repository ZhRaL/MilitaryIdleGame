using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoldierWalkUtil
    {
        public Soldier Soldier { get; set; }
        public Transform Target { get; set; }
        public Action Action { get; set; }
        public Action<SoldierWalkUtil> FinishedListener { get; set; }
        Vector3 _targetDirection;
        private float distanceToTarget;
        private Transform[] waypoints;
        private int currentTargetIndex = 0;


        public SoldierWalkUtil(Soldier soldier, Transform target, Action action,
            Action<SoldierWalkUtil> finishedListener, float distanceToTarget = .2f, Transform[] waypoints = null)
        {
            Soldier = soldier;
            Target = target;
            Action = action;
            FinishedListener = finishedListener;
            this.distanceToTarget = distanceToTarget;
            this.waypoints = waypoints;
            if (waypoints == null)
                _targetDirection = (Target.position - soldier.transform.position).normalized;
            else
            {
                Target = this.waypoints[currentTargetIndex];
                double distance = (Soldier.transform.position - Target.position).magnitude;
                if (distance < distanceToTarget)
                {
                    Target = waypoints[++currentTargetIndex];
                }

                _targetDirection = (this.waypoints[currentTargetIndex].position - soldier.transform.position).normalized;
            }
        }
        
        private void ReachedTarget()
        {
            if (waypoints == null || currentTargetIndex == waypoints.Length-1)
            {
                FinishedListener.Invoke(this);
                Action.Invoke();
            }
            else
            {
                Target = waypoints[++currentTargetIndex];
                _targetDirection = (Target.position - Soldier.transform.position).normalized;
                
            }
        }

        public void Update()
        {
            Vector3 forward = Target.position - Soldier.transform.position;
            Quaternion neededRotation = Quaternion.LookRotation(forward);
            Soldier.transform.rotation =
                Quaternion.RotateTowards(Soldier.transform.rotation, neededRotation, Time.deltaTime * 200f);

            float distance = Vector3.Distance(Soldier.transform.position, Target.position);
            float step = Soldier.Speed / distance * Time.deltaTime;
            
            Soldier.transform.position = Vector3.Lerp(Soldier.transform.position, Target.position, step);
            
            double distance2 = (Soldier.transform.position - Target.position).magnitude;
            if (distance2 < distanceToTarget)
            {
                Soldier.transform.position = Target.position;
                ReachedTarget();
            }
            // schießen übers Ziel hinaus
        }
    }
}