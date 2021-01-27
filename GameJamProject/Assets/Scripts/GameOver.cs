using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject failUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.Instance.skinTime <= 0) 
        {
            failUI.SetActive(true);
        }

        if (PlayerMovement.Instance.hasBeenSpotted == true) 
        {
            failUI.SetActive(true);
        }
    }

    public void Replay() 
    {
        SceneManager.LoadScene("Prototype");
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
