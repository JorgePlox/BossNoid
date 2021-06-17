using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   //lives
    public Text livesText;

    ////Score
    //public Text bestTime;
    //public Text currentTime;


    public static UIManager sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    private void Start()
    {
        //DisplayBestTime();
    }
    private void Update()
    {
        //UpdateCurrentTime();
    }

    public void UpdateLives(string lives)
    {
        if (livesText != null)
        {
            livesText.text = "X " + lives;
        }
    }

    //void UpdateCurrentTime()
    //{
    //    if (currentTime != null)
    //    {
    //        float time = GameManager.sharedInstance.myTime;
    //        string minutes = ((int)time / 60).ToString();
    //        string seconds = (time % 60).ToString("f0");

    //        currentTime.text = "Time:\n" + minutes + ":" + seconds;
    //    }
    //}



}
