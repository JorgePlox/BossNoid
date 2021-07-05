using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance; 
    public int blockCount = 0;

    //pause
    public bool isPaused = false;

    //puede tirar la bola?
    public bool canThrowBall = true;
    GameObject ballObject;

    //Conteo del tiempo
    public float myTime = 0.0f;
    public float bestTime;

    //PauseMenu
    [SerializeField] Canvas pauseCanvas;

    //Death
    public bool isGameOver = false;
    [SerializeField] Canvas deathCanvas;



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
        RestartGame();
        CountBlocks();
        bestTime = GetLevelBestTime();
        ResumeGame();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
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

        if (!isPaused || canThrowBall || !isGameOver)
        {
            myTime += Time.deltaTime;
        }

        if (isGameOver && Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                return;
            LevelManager.sharedInstance.RestartScene();
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

            case Levels.Dracula:
                return PlayerPrefs.GetFloat("BestTimeDracula", 5999f);

            case Levels.Clown:
                return PlayerPrefs.GetFloat("BestTimeClown", 5999f);
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

            case Levels.Dracula:
                PlayerPrefs.SetFloat("BestTimeDracula", newTime);
                break;

            case Levels.Clown:
                PlayerPrefs.SetFloat("BestTimeClown", newTime);
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
                break;

            case Levels.Alien:
                PlayerPrefs.SetInt("FinishAlien", 1);
                break;

            case Levels.Dracula:
                PlayerPrefs.SetInt("FinishDracula", 1);
                break;

            case Levels.Clown:
                PlayerPrefs.SetInt("FinishClown", 1);
                break;

        }
    }


    public void GameOver()
    {
        isGameOver = true;
        canThrowBall = false;

        if (deathCanvas != null)
        {
            deathCanvas.enabled = true;
        }
    }

    void RestartGame()
    {
        deathCanvas.enabled = false;
        isGameOver = false;
        myTime = 0.0f;
    }

}
