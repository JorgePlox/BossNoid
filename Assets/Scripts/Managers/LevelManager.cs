using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }



    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


    public void RestartScene()
    {
        string actualScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(actualScene);
    }

}
