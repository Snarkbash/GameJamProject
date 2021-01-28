using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public AI_Roaming[] citizens;

    public float spawnGap;

    [SerializeField] float spawnTimer;

    void Update()
    {
        if(spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            Instantiate(citizens[Random.Range(0, citizens.Length)], transform.position, transform.rotation);

            spawnTimer = spawnGap;
        }
    }
}
