using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D m_rigidbody;
    public static PlayerController sharedInstance;

    private bool isParalized = false;

    //Movement
    [SerializeField] float speed = 10.0f;

    //Lives
    int playerLives = 3;

    SpriteRenderer playerRenderer;
    AudioSource playerAudio;

    private void Awake()
    {
        if (sharedInstance == null)
        { 
            sharedInstance = this; 
        }

        m_rigidbody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAudio = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        if (!isParalized)
        {
            float hMov = Input.GetAxisRaw("Horizontal");

            m_rigidbody.velocity = Vector2.right * hMov * speed;
        }

        else
        {
            m_rigidbody.velocity = Vector2.zero;
        }
    }


    //EL jugador pierde una vida
    public void LossLive()
    {
        playerLives--;
        if (playerLives == 0)
        {
            GameOver();
        }
    }

    //El jugador pierde todas las vidas
    public void GameOver()
    {
        LevelManager.sharedInstance.ChangeScene("MainMenu");
        playerLives = 3;
    }

    public void ParalizePlayer(float time)
    {
        StartCoroutine(ParalizeTime(time));
    }
    
    IEnumerator ParalizeTime(float time)
    {
        isParalized = true;
        if (playerRenderer != null) playerRenderer.color = Color.grey;
        if (playerAudio != null) playerAudio.Play();
        yield return new WaitForSeconds(time);
        if (playerRenderer != null) playerRenderer.color = Color.white;
        isParalized = false;
    }

}
