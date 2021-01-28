using UnityEngine;

[CreateAssetMenu(fileName = "NewPlot", menuName = "New Roaming Data", order = 1)]

public class AIRoamingData : ScriptableObject
{
    public Vector3[] targetLocations;

    public AI_Roaming.StopPointAndTime[] stopPointAndTime;
}
