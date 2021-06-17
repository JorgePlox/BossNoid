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

    public AudioClip paralizeClip;
    public AudioClip deathClip;

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

    private void Start()
    {
        UIManager.sharedInstance.UpdateLives(playerLives.ToString());
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
        UIManager.sharedInstance.UpdateLives(playerLives.ToString());
        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            GameManager.sharedInstance.ResetBall();
        }
    }

    //El jugador pierde todas las vidas
    public void GameOver()
    {

        StartCoroutine(DeathRoutine());
    }

    public void ParalizePlayer(float time)
    {
        StartCoroutine(ParalizeTime(time));
    }
    
    IEnumerator ParalizeTime(float time)
    {
        isParalized = true;
        if (playerRenderer != null) playerRenderer.color = Color.grey;
        if (playerAudio != null && paralizeClip != null) playerAudio.PlayOneShot(paralizeClip);
        yield return new WaitForSeconds(time);
        if (playerRenderer != null) playerRenderer.color = Color.white;
        isParalized = false;
    }

    IEnumerator DeathRoutine()
    {
        if (playerAudio != null && deathClip != null) playerAudio.PlayOneShot(deathClip);

        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;

        isParalized = true;

        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }

        yield return new WaitForSeconds(2.0f);
        LevelManager.sharedInstance.ChangeScene("LevelSelector");
    }

}
