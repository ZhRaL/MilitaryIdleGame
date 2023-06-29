using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public GameObject parentRoute;
    public Transform[] path;
    private Animator anim;
    [SerializeField]
    private soldierType _soldierType;
    public int currentTarget = 0;
    public float speed;
    private bool isRunning = false;
    public bool isWaiting;
    Vector3 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
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
        anim.speed = speed/4;
        anim.SetBool("isRunning", true);
        isRunning = true;
        targetDirection = (path[currentTarget].position - transform.position).normalized;
        isWaiting = false;
    }

    private void Update()
    {

        if (isRunning)
        {
            transform.position += targetDirection * (speed * Time.deltaTime);
            Vector3 forward = path[currentTarget].position - transform.position;
            Quaternion neededRotation = Quaternion.LookRotation(forward);    
            transform.rotation= Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 200f );        
        }


        if (!isWaiting)
            CheckDistance();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckDistance()
    {
        if((transform.position - path[currentTarget].position).magnitude < 1)
        {
            anim.SetBool("isRunning", false);
            isRunning = false;
            isWaiting = true;
            Debug.Log("Goal");
            path[currentTarget].GetComponent<RoutingPoint>().DoAction(this);
        }
    }

    public void ActionDone()
    {
        StartNextRun();
    }
    
    public void StartNextRun()
    {
        currentTarget = ++currentTarget % parentRoute.transform.childCount;

        Run();
    }

    public enum soldierType {
        ARMY,MARINE,AIRFORCE
    }
}
