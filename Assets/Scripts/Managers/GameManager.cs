using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance; 
    public int blockCount = 0;

    //pause
    bool isPaused = false;

    //puede tirar la bola?
    public bool canThrowBall = true;
    GameObject ballObject;

    //Conteo del tiempo
    public float myTime = 0.0f;
    public float bestTime;

    //PauseMenu
    public Canvas pauseCanvas;



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
        myTime = 0.0f;
        bestTime = GetLevelBestTime();
        ResumeGame();


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

        if (!isPaused || canThrowBall)
        {
            myTime += Time.deltaTime;
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
            WinRound();

        }
    }

    void WinRound()
    {
        SetFinishedLevel();
        float currentBestTime = GetLevelBestTime();
        if (myTime < currentBestTime)
        {
            SetLevelBestTime(myTime);
        }
        
        LevelManager.sharedInstance.ChangeScene("LevelSelector");

    }

    public void ResumeGame()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = false;
        }
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void PauseGame()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.enabled = true;
        }
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResetBall()
    {
        ballObject.GetComponent<Ball>().ResetLaunch();
    }


    float GetLevelBestTime()
    {
        Levels currentLevel = LevelManager.currentLevel;
        switch (currentLevel)
        {
            case Levels.SkeleBoss:
                return PlayerPrefs.GetFloat("BestTimeSkele", 5999f);

            case Levels.Alien:
                return PlayerPrefs.GetFloat("BestTimeAlien", 5999f);
        }

        return 5999f;
    }

    void SetLevelBestTime(float newTime)
    {
        Levels currentLevel = LevelManager.currentLevel;
        switch (currentLevel)
        {
            case Levels.SkeleBoss:
               PlayerPrefs.SetFloat("BestTimeSkele", newTime);
                break;

            case Levels.Alien:
                PlayerPrefs.SetFloat("BestTimeAlien", newTime);
                break;
        }
    }

    void SetFinishedLevel()
    {
        Levels currentLevel = LevelManager.currentLevel;
        switch (currentLevel)
        {
            case Levels.SkeleBoss:
                PlayerPrefs.SetInt("FinishSkele", 1);
                Debug.Log("finish");
                break;

            case Levels.Alien:
                PlayerPrefs.SetInt("FinishAlien", 1);
                break;

        }
    }

}
