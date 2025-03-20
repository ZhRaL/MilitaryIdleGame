using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Util;

public class Soldier : MonoBehaviour
{
    [SerializeField] private string _soldierName;
    
    public string SoldierName
    {
        get { return _soldierName; }
        set
        {
            OnNameChanged?.Invoke(value);
            _soldierName = value;
        }
    }

    public UnityAction<string> OnNameChanged;
    public GameObject parentRoute;
    public Transform[] path;
    public Animator anim;
    [SerializeField] private DefenseType soldierTypeEnum;

    public int currentTarget = 0;
    private float baseSpeed = 2f;
    [SerializeField] private float speed;
    private bool isRunning;
    public bool isWaiting;
    Vector3 targetDirection;
    public float distanceToTarget;
    public GameObject RadialBarPrefab;

    public int Index => transform.GetSiblingIndex();

    public float Crit { get; set; }

    public int MissionReward => 0; // DataProvider.GetReward(LVL_Reward)

    public delegate void LevelUpEventHandler(int newLevel);

    // Ereignis für Levelaufstieg
    // public event LevelUpEventHandler OnLevelUp;

    public int LVL_Crit, LVL_Speed, LVL_Reward;

    public float Speed => baseSpeed + 2 * (LVL_Speed / 100);

    public DefenseType SoldierType
    {
        get => soldierTypeEnum;
        set => soldierTypeEnum = value;
    }

    void Start()
    {
        distanceToTarget = .15f * Speed;

        anim = GetComponent<Animator>();
        // Creating Route
        path = new Transform[parentRoute.transform.childCount];
        for (int i = 0; i < parentRoute.transform.childCount; i++)
        {
            path[i] = parentRoute.transform.GetChild(i).transform;
        }

        Run();
    }

    void Run()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Run"))
        {
            anim.speed = Speed / 4;
        }
        isRunning = true;
        targetDirection = (path[currentTarget].position - transform.position).normalized;
        isWaiting = false;
    }

    private void Update()
    {
        if (!isWaiting)
            CheckDistance();

        if (isRunning)
        {
            // transform.position += targetDirection * (Speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, path[currentTarget].position);
            var step = Speed / distance * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, path[currentTarget].position, step);

            Vector3 forward = path[currentTarget].position - transform.position;
            Quaternion neededRotation = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 200f);
        }


    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckDistance()
    {
        if ((transform.position - path[currentTarget].position).magnitude < distanceToTarget)
        {
            // anim.SetBool("isRunning", false);
            isRunning = false;
            isWaiting = true;
            path[currentTarget].GetComponent<RoutingPoint>().DoAction(this);
        }
    }

    public void StartNextRun()
    {
        currentTarget = ++currentTarget % parentRoute.transform.childCount;

        Run();
    }

    public Item ToItem(SoldierUpgradeType type)
    {
        SoldierItem item;

        switch (type)
        {
            case SoldierUpgradeType.SPEED:
                item = gameObject.AddComponent<SoldierSpeedItem>();
                item.Level = LVL_Speed;
                break;
            case SoldierUpgradeType.REWARD:
                item = gameObject.AddComponent<SoldierRewardItem>();
                item.Level = LVL_Reward;
                break;
            case SoldierUpgradeType.CRIT:
                item = gameObject.AddComponent<SoldierCritItem>();
                item.Level = LVL_Crit;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        item.Soldier = this;
        item.Init();
        return item;
    }

    public static UpgradeType ToUpgradeType(SoldierUpgradeType type)
    {
        return type switch
        {
            SoldierUpgradeType.CRIT => UpgradeType.SOLDIER_CRIT,
            SoldierUpgradeType.SPEED => UpgradeType.SOLDIER_SPEED,
            SoldierUpgradeType.REWARD => UpgradeType.SOLDIER_REWARD,
            _ => throw new NotImplementedException()
        };
    }

    public enum SoldierUpgradeType
    {
        SPEED,
        REWARD,
        CRIT
    }
}