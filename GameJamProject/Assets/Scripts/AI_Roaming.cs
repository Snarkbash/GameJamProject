using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Roaming : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform[] targetLocations;

    public float reachRange;

    public bool loopPath;

    public int[] stopPoints;
    public float stopTime;

    [Header("Run Time")]
    [SerializeField] int currentTarget;
    [SerializeField] float stopTimer;
    [SerializeField] bool stopped;

    private void Start()
    {
        nav.SetDestination(targetLocations[0].position);
    }

    private void Update()
    {
        if ((transform.position - targetLocations[currentTarget].position).magnitude <= reachRange)
        {
            for (int i = 0; i < stopPoints.Length; i++)
            {
                if (stopPoints[i] == currentTarget && !stopped)
                {
                    stopTimer = stopTime;
                    stopped = true;
                }
            }

            if (stopTimer > 0)
            {
                stopTimer -= Time.deltaTime;
            }
            else
            {
                if (currentTarget + 1 != targetLocations.Length)
                {
                    currentTarget++;
                    nav.SetDestination(targetLocations[currentTarget].position);
                }
                else if (loopPath)
                {
                    currentTarget = 0;
                    nav.SetDestination(targetLocations[currentTarget].position);
                }

                stopped = false;
            }
        }
    }
}
