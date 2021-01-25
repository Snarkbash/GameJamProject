using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //WOW
    public NavMeshAgent playerNav;
    public LayerMask ground;
    public LayerMask monster;
    
    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, ground))
            {
                playerNav.destination = hit.point;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, monster))
            {
                playerNav.destination = hit.point;
                Debug.Log("Interact");
            }
        }
    }
}
