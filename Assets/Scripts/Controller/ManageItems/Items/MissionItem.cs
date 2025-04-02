using System.Collections;
using System.Linq;
using Controller.ManageItems.Items;
using Interfaces;
using Manager;
using UnityEngine;
using UnityEngine.VFX;
using Util;

namespace DefaultNamespace
{
    public abstract class MissionItem : Item
    {
        private GameObject startEffect;
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

        public abstract Transform Waypoints { get; set; }
        private Soldier _soldier;
        private SoldierWalkUtil wayBack;
        public GameObject Model;

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
            Model.GetComponent<Animator>().SetTrigger("Mission_Start");
            if(startEffect!=null)
                startEffect.SetActive(true);
            // var x = startEffect.GetComponent<VisualEffect>();
            //x.Play();
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            Model.GetComponent<Animator>().SetTrigger("Mission_End");
        }

        public void getBackDelayed()
        {
            StartCoroutine(ExecuteAfterTime(calculateWaitingDuration()));
        }

        private float calculateWaitingDuration()
        {
            var anim = Model.GetComponent<Animator>();
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
            int amount = GameManager.INSTANCE.DataProvider.GetReward(this, true);
            float tempAmount = amount * (1+(float)(_soldier.LVL_Reward * 2) / 100);
            amount = (int) tempAmount;

            if (IsCrit())
            {
                amount *= 2;
            }

            EffectManager.Instance.ShowReward(_soldier.transform, amount);
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
        
        public void LoadModel()
        {
            // Ensure it is only registered once
            OnMoneyLevelUp -= CheckModel;
            OnMoneyLevelUp += CheckModel; 
            Model = Instantiate(ModelManager.INSTANCE.GetModelPrefab(ObjectType.defenseType, _moneyLevel),transform);
            
            VehicleAnimatorHook hook = Model.GetComponent<VehicleAnimatorHook>();
            if(hook==null) 
                return;
            hook.Init(this);
            startEffect = hook.SmokeEffect;
        }

        public void CheckModel(int discard)
        {
            if(!SliderValues.IsLevelUpNumber(_moneyLevel)) 
                return;
            Destroy(Model);
            LoadModel();
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