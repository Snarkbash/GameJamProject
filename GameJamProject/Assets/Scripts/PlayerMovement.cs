using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance { get; private set; }


    public delegate void OnFocusChanged(Interaction newFocus);
    public OnFocusChanged onFocusChangedCallback;

    public Interaction focus;
    Transform target;

    public NavMeshAgent playerNav;
    public LayerMask ground;
    public LayerMask interactionPoint;
    public LayerMask skinPoint;
    public bool hasBeenSpotted = false;

    public bool interact = false;
    public int skinType = 0;
    public int activeSkin = 0;
    public float skinTime = 30.0f;

    public Slider timeSlider;
    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
        Instance = this;
    }
    void Update()
    {
        skinTime -= Time.deltaTime;
        timeSlider.value = skinTime / 30;

        if (target != null)
        {
            FaceTarget();
        }
        else 
        {
            playerNav.updateRotation = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, ground))
            {
                target = null;
                focus = null;
                playerNav.destination = hit.point;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, interactionPoint))
            {
                playerNav.destination = hit.point;			
                SetFocus(hit.collider.GetComponent<Interaction>());

            }
        }
    }

    void SetFocus(Interaction newFocus)
    {
        if (onFocusChangedCallback != null)
            onFocusChangedCallback.Invoke(newFocus);
            playerNav.stoppingDistance = newFocus.radius * .8f;
            playerNav.updateRotation = false;
            target = newFocus.interactionTransform;

        // If our focus has changed
        if (focus != newFocus && focus != null)
        {
            // Let our previous focus know that it's no longer being focused
            focus.OnDefocused();
          
        }
      
        // Set our focus to what we hit
        // If it's not an interactable, simply set it to null
        focus = newFocus;

        if (focus != null)
        {
            // Let our focus know that it's being focused
            focus.OnFocused(transform);
        }

    }
    public void MoveToPoint(Vector3 point)
    {
        playerNav.SetDestination(point);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
