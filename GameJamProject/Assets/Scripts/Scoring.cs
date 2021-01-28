using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int score;
    public int savedScore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score += 1;
    }
}
