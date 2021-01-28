using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Roaming : MonoBehaviour
{
    [System.Serializable]
    public class StopPointAndTime
    {
        public int stopPoint;
        public float stopTime;
    }

    public NavMeshAgent nav;
    public AIRoamingData data;

    public float reachRange;

    public bool loopPath;

    [Header("Run Time")]
    [SerializeField] int currentTarget;
    [SerializeField] float stopTimer;
    [SerializeField] bool stopped;

    private void Start()
    {
        if (nav == null)
        {
            nav = GetComponent<NavMeshAgent>();
        }

        nav.SetDestination(data.targetLocations[0]);
    }

    private void Update()
    {
        if ((transform.position - data.targetLocations[currentTarget]).magnitude <= reachRange)
        {
            for (int i = 0; i < data.stopPointAndTime.Length; i++)
            {
                if (data.stopPointAndTime[i].stopPoint == currentTarget && !stopped)
                {
                    stopTimer = data.stopPointAndTime[i].stopTime;
                    stopped = true;
                }
            }

            if (stopTimer > 0)
            {
                stopTimer -= Time.deltaTime;
            }
            else
            {
                if (currentTarget + 1 != data.targetLocations.Length)
                {
                    currentTarget++;
                    nav.SetDestination(data.targetLocations[currentTarget]);
                }
                else if (loopPath)
                {
                    currentTarget = 0;
                    nav.SetDestination(data.targetLocations[currentTarget]);
                }

                stopped = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Map")
        {
            Destroy(gameObject);
        }
    }
}
