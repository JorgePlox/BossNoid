using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimeManager : MonoBehaviour
{
    public static BestTimeManager sharedInstance;

    //BestTimesTexts
    [SerializeField] Text SkeleBossBestTime;
    [SerializeField] Text AlienBestTime;
    [SerializeField] Text DraculaBestTime;
    [SerializeField] Text ClownBestTime;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;

        }

    }

    private void Start()
    {
        DisplayBestTimes();
    }

    void DisplayBestTimes()
    {
        if (SkeleBossBestTime != null)
        {
            if (PlayerPrefs.GetInt("FinishSkele", 0) == 1)
            {
                SkeleBossBestTime.enabled = true;
                float time = PlayerPrefs.GetFloat("BestTimeSkele", 5999f);
                
                string minutes = ((int)time / 60).ToString();
                string seconds = (time % 60).ToString("f0");

                SkeleBossBestTime.text = minutes + ":" + seconds;
            }
            else 
            {
                SkeleBossBestTime.enabled = false;
            }
        }

        if (AlienBestTime != null)
        {
            if (PlayerPrefs.GetInt("FinishAlien", 0) == 1)
            {
                AlienBestTime.enabled = true;
                float time = PlayerPrefs.GetFloat("BestTimeAlien", 5999f);

                string minutes = ((int)time / 60).ToString();
                string seconds = (time % 60).ToString("f0");

                AlienBestTime.text = minutes + ":" + seconds;
            }
            else
            {
                AlienBestTime.enabled = false;
            }
        }

        if (DraculaBestTime != null)
        {
            if (PlayerPrefs.GetInt("FinishDracula", 0) == 1)
            {
                AlienBestTime.enabled = true;
                float time = PlayerPrefs.GetFloat("BestTimeDracula", 5999f);

                string minutes = ((int)time / 60).ToString();
                string seconds = (time % 60).ToString("f0");

                DraculaBestTime.text = minutes + ":" + seconds;
            }
            else
            {
                DraculaBestTime.enabled = false;
            }
        }

        if (ClownBestTime != null)
        {
            if (PlayerPrefs.GetInt("FinishClown", 0) == 1)
            {
                ClownBestTime.enabled = true;
                float time = PlayerPrefs.GetFloat("BestTimeClown", 5999f);

                string minutes = ((int)time / 60).ToString();
                string seconds = (time % 60).ToString("f0");

                ClownBestTime.text = minutes + ":" + seconds;
            }
            else
            {
                ClownBestTime.enabled = false;
            }
        }

    }


}
