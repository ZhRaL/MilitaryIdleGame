using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

public class Soldier : MonoBehaviour
{
    public string SoldierName { get; set; }
    public GameObject parentRoute;
    public Transform[] path;
    public Animator anim;
    [SerializeField] private DefenseType soldierTypeEnum;

    public int currentTarget = 0;
    [SerializeField]
    private float speed;
    private bool isRunning;
    public bool isWaiting;
    Vector3 targetDirection;
    public float distanceToTarget;
    public GameObject RadialBarPrefab;

    public int Index => transform.GetSiblingIndex();

    public float Crit { get; set; }

    public float MovementSpeed
    {
        get => speed;
        set => speed = value;
    }

    public int MissionReward => 0; // DataProvider.GetReward(LVL_Reward)
    
    public delegate void LevelUpEventHandler(int newLevel);
    
    // Ereignis für Levelaufstieg
    public event LevelUpEventHandler OnLevelUp;

    public int LVL_Crit, LVL_Speed, LVL_Reward;

    public float Speed
    {
        get => speed;
        set
        {
            distanceToTarget = .15f * value;
            speed = value;
        }
    }

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
        anim.speed = Speed / 4;
        anim.SetBool("isRunning", true);
        isRunning = true;
        targetDirection = (path[currentTarget].position - transform.position).normalized;
        isWaiting = false;
    }

    private void Update()
    {
        if (isRunning)
        {
            transform.position += targetDirection * (Speed * Time.deltaTime);
            Vector3 forward = path[currentTarget].position - transform.position;
            Quaternion neededRotation = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 200f);
        }


        if (!isWaiting)
            CheckDistance();
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
        SoldierItem item = gameObject.AddComponent<SoldierItem>();
        item.Soldier = this;
        switch (type)
        {
            case SoldierUpgradeType.SPEED:
                item.Level = LVL_Speed;
                break;
            case SoldierUpgradeType.REWARD:
                item.Level = LVL_Reward;
                break;
            case SoldierUpgradeType.CRIT:
                item.Level = LVL_Crit;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        return item;
    }

    public enum SoldierUpgradeType
    {
        SPEED,REWARD,CRIT
    }
}
