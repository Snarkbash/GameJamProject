using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrans : MonoBehaviour
{
    public Transform playerPos;
    public LayerMask building;

    public GameObject hitBuilding;
    public Color buildingCol;
    public Renderer buildingMat;
    public Material buildingColourOrigin;
    void Start()
    {
        
    }

    void Update()
    {
       Vector3 screenPos = Camera.main.WorldToScreenPoint(playerPos.transform.position);
       Ray rayToPlayer = Camera.main.ScreenPointToRay(screenPos);
       RaycastHit hit;
       Debug.DrawRay(rayToPlayer.origin, rayToPlayer.direction * 50, Color.yellow);

        if (Physics.Raycast(rayToPlayer, out hit, 1000, building))
        {
           hitBuilding = hit.collider.gameObject;
           buildingCol = new Color(0, 0, 0, 0);
           buildingMat = hitBuilding.GetComponent<Renderer>();
           buildingMat.material.color = buildingCol;
        }
        else 
        {
            if (hitBuilding != null) 
            {
                buildingCol = new Color(192, 110, 90, 255);
                buildingMat.material.color = buildingColourOrigin.color;
            }
        }
    }
}
