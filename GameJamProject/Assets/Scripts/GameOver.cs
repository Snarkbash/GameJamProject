using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { get; private set; }

    public GameObject failUI;
    public bool hasFailed = false;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.Instance.skinTime <= 0) 
        {
            failUI.SetActive(true);
            hasFailed = true;
        }

        if (PlayerMovement.Instance.hasBeenSpotted == true) 
        {
            failUI.SetActive(true);
            hasFailed = true;
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
