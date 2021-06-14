using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance; 
    int blockCount = 0;

    //pause
    bool isPaused = false;

    //puede tirar la bola?
    public bool canThrowBall = true;
    GameObject ballObject;



    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        ballObject = GameObject.Find("Ball");
    }

    private void Start()
    {
        CountBlocks();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }

            else if (!isPaused)
            {
                PauseGame();
            }
        }
    }

    void CountBlocks()
    {
        blockCount = GameObject.FindGameObjectsWithTag("Destructable").Length;
    }

    public void DecreaseBlockCount()
    {
        blockCount--;
        if (blockCount == 0)
        {
            LevelManager.sharedInstance.ChangeScene("MainMenu");
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResetBall()
    {
        ballObject.GetComponent<Ball>().ResetLaunch();
    }

}
