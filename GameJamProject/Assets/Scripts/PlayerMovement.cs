using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //WOW
    public NavMeshAgent playerNav;
    public float moveSpeed = 6.0f;
    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                playerNav.destination = hit.point;
            }
        }
    }
}
