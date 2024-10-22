using System.Collections;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.VFX;
using Util;

namespace DefaultNamespace
{
    public abstract class MissionItem : Item
    {
        public GameObject startEffect;
        public int MoneyLevel
        {
            get => _moneyLevel;
            set
            {
                _moneyLevel = value;
                OnMoneyLevelUp?.Invoke(value);
            }
        }

        private int _moneyLevel = 1;

        public Animation anim1, anim2;

        public abstract Transform Waypoints { get; set; }
        private Soldier _soldier;
        private SoldierWalkUtil wayBack;

        public delegate void MoneyLevelUpEventHandler(int newLevel);

        // Ereignis für Levelaufstieg
        public event MoneyLevelUpEventHandler OnMoneyLevelUp;


        public void MoneyUpgrade()
        {
            MoneyLevel++;
            Unlocked = true;
        }

        public override void SoldierSitDown(Soldier soldier)
        {
            _soldier = soldier;
            _soldier.transform.Rotate(Vector3.up, 180);
            soldier.gameObject.SetActive(false);
            if(audioSource!=null && AudioManager.Instance.SoundEnabled)
                audioSource.Play();
            MissionStart();
        }

        public void MissionStart()
        {
            GetComponent<Animator>().SetTrigger("Mission_Start");
            if(startEffect!=null)
                startEffect.SetActive(true);
            // var x = startEffect.GetComponent<VisualEffect>();
            //x.Play();
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            GetComponent<Animator>().SetTrigger("Mission_End");
        }

        public void getBackDelayed()
        {
            StartCoroutine(ExecuteAfterTime(calculateWaitingDuration()));
        }

        private float calculateWaitingDuration()
        {
            var anim = GetComponent<Animator>();
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            float animationDuration = clips.Sum(cl => cl.averageDuration);

            // Duration - AnimationDuration
            return TimeNeeded() - animationDuration;
        }

        public void MissionEnd()
        {
            if(audioSource!=null)
                audioSource.Stop();
            Reward();
            if(startEffect!=null)
                startEffect.SetActive(false);
            LetSoldierMove();
        }

        private void LetSoldierMove()
        {
            _soldier.gameObject.SetActive(true);
            _soldier.anim.SetBool("isRunning", true);
            Occupied = false;
            Parent.ItemIsFree();
            wayBack = new SoldierWalkUtil(_soldier, null, () => RoutingPoint.LetSoldierMove(_soldier), RemoveWayBack,
                .2f,
                Waypoints.GetAllChildren().Reverse().ToArray());
        }

        public void Reward()
        {
            var amount = GameManager.INSTANCE.DataProvider.GetReward(this, true);
            amount *= (_soldier.LVL_Reward * 2) / 100;
            if (IsCrit())
            {
                //TODO Anim?
                amount *= 2;
            }

            GameManager.INSTANCE.Gold += amount;
        }

        private bool IsCrit()
        {
            var val = Random.Range(0, 100);
            return val <= _soldier.LVL_Crit / 2;
        }

        private void RemoveWayBack(SoldierWalkUtil util)
        {
            wayBack = null;
        }

        private void Update()
        {
            wayBack?.Update();
        }

        public override JsonItem ToJson()
        {
            return new MissionItemJO()
            {
                Json_Level = Level,
                MoneyLevel = MoneyLevel
            };
        }
    }
}