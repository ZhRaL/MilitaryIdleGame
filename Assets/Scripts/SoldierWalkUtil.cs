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


        public SoldierWalkUtil(Soldier soldier, Transform target, Action action, Action<SoldierWalkUtil> finishedListener)
        {
            Soldier = soldier;
            Target = target;
            _targetDirection = (Target.position - soldier.transform.position).normalized;
            Action = action;
            FinishedListener = finishedListener;
        }

        private void ReachedTarget()
        {
            FinishedListener.Invoke(this);
            Action.Invoke();
        }

        public void Update()
        {
            Soldier.transform.position += _targetDirection * (Soldier.Speed * Time.deltaTime);
            Vector3 forward = Target.position - Soldier.transform.position;
            Quaternion neededRotation = Quaternion.LookRotation(forward);
            Soldier.transform.rotation = Quaternion.RotateTowards(Soldier.transform.rotation, neededRotation, Time.deltaTime * 200f);

            if ((Soldier.transform.position - Target.position).magnitude < .2f)
            {
                ReachedTarget();
            }
        }
    }
}