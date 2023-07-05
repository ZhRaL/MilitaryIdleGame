using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Soldier : MonoBehaviour
{
    public GameObject parentRoute;
    public Transform[] path;
    public Animator anim;
    [SerializeField] private SoldierTypeEnum soldierTypeEnum;

    public int currentTarget = 0;
    [SerializeField]
    private float speed;
    private bool isRunning;
    public bool isWaiting;
    Vector3 targetDirection;
    public float distanceToTarget;
    public GameObject RadialBarPrefab;
    
    public float Speed
    {
        get => speed;
        set
        {
            distanceToTarget = .15f * value;
            speed = value;
        }
    }

    public SoldierTypeEnum SoldierType
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
            Debug.Log("Goal");
            path[currentTarget].GetComponent<RoutingPoint>().DoAction(this);
        }
    }

    public void StartNextRun()
    {
        currentTarget = ++currentTarget % parentRoute.transform.childCount;

        Run();
    }

    public enum SoldierTypeEnum
    {
        ARMY,
        MARINE,
        AIRFORCE
    }
}