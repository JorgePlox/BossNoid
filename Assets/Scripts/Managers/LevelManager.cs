using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels
{
    MainMenu,
    LevelMenu,
    SkeleBoss,
    Alien
}
public class LevelManager : MonoBehaviour
{
    public static Levels currentLevel;


    //Transition
    [SerializeField] float transitionDuration = 0.5f;
    public Animator transition;
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip transitionClip;


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
        ChangeLevel(scene);
        StartCoroutine(TransitionScene(scene));
    }


    public void RestartScene()
    {
        string actualScene = SceneManager.GetActiveScene().name;
        StartCoroutine(TransitionScene(actualScene));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ChangeLevel(string level)
    {
        if (level == "MainMenu")
        {
            currentLevel = Levels.MainMenu;
        }
        else if (level == "LevelSelector")
        {
            currentLevel = Levels.LevelMenu;
        }
        else if (level == "SkeleBoss")
        {
            currentLevel = Levels.SkeleBoss;
        }

    }

    IEnumerator TransitionScene(string scene)
    {
        if (GameManager.sharedInstance != null)  GameManager.sharedInstance.ResumeGame();
        transition.SetTrigger("StartTransition");
        if (m_AudioSource != null && transitionClip != null)
            m_AudioSource.PlayOneShot(transitionClip);

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(scene);
    }

}
