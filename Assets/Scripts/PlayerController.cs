using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D m_rigidbody;
    public static PlayerController sharedInstance;

    //Movement
    [SerializeField] float speed = 10.0f;

    //Lives
    int playerLives = 3;

    //Paralize
    private bool isParalized = false;
    public AudioClip paralizeClip;
    Coroutine paralizeCoroutine = null;

    //Slow
    private bool isSlowed = false;
    float slowTime = 0.0f;
    [SerializeField] AudioClip slowedClip;
    float slowFactor = 1.0f;


    //Invisible
    bool isInvisible = false;
    Coroutine invisibleCoroutine = null;
    [SerializeField] AudioClip invisibleClip;

    //Stunned
    bool isStunned = false;
    //Coroutine stunCoroutine = null;
    [SerializeField] AudioClip StunnedClip;



    SpriteRenderer playerRenderer;
    AudioSource playerAudio;

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

        if (!isParalized && playerLives > 0)
            {

                float hMov = Input.GetAxisRaw("Horizontal");


                Vector2 newVelocity = Vector2.right * hMov * speed;


                if (isSlowed)
                    newVelocity = SlowSpeed(newVelocity);

                if (isStunned)
                    newVelocity = -newVelocity;

                m_rigidbody.velocity = newVelocity;
            }

        else
            {
                m_rigidbody.velocity = Vector2.zero;
            }

    }


    //EL jugador pierde una vida
    public void LossLive()
    {
        slowTime = 0.0f;
        playerLives--;
        UIManager.sharedInstance.UpdateLives(playerLives.ToString());
        if (playerLives <= 0)
        {
            GameOver();
        }
        else if(!GameManager.sharedInstance.isGameOver)
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
        if (paralizeCoroutine != null)
        {
            StopCoroutine(paralizeCoroutine);
            paralizeCoroutine = StartCoroutine(ParalizeTime(time));
        }
        else
        {
            paralizeCoroutine = StartCoroutine(ParalizeTime(time));
        }
        
    }

    
    IEnumerator ParalizeTime(float time)
    {
        isParalized = true;
        if (playerRenderer != null && (!isInvisible && !isStunned)) playerRenderer.color = Color.grey;
        if (playerAudio != null && paralizeClip != null) playerAudio.PlayOneShot(paralizeClip);
        yield return new WaitForSeconds(time);
        if (playerRenderer != null && (!isInvisible && !isStunned)) playerRenderer.color = Color.white;
        isParalized = false;
    }

    public void SlowPlayer(float time)
    {
        isSlowed = true;
        slowTime = time;
        if (playerAudio != null && slowedClip != null) playerAudio.PlayOneShot(slowedClip);
        
        if (playerRenderer != null && (!isInvisible && !isStunned))
        {
            playerRenderer.color = new Vector4(playerRenderer.color.r * 0.75f, 1.0f, playerRenderer.color.b * 0.75f, 1.0f);
        }
        
        slowFactor = slowFactor * 0.75f;

        if (slowFactor <= 0.3f)
        {
            slowTime = 1.0f;
            slowFactor = 0.3f;
        }

    }

    Vector2 SlowSpeed(Vector2 enterSpeed)
    {
        if (slowTime > 0)
        {
            enterSpeed = enterSpeed * slowFactor;
            slowTime -= Time.deltaTime;
        }

        else 
        {
            slowTime = 0.0f;
            isSlowed = false;
            if (playerRenderer != null && (!isInvisible && !isStunned)) playerRenderer.color = Color.white;
            slowFactor = 1.0f;
        }

        return enterSpeed;
    }

    public void TurnInvisible(float time)
    {
        if (invisibleCoroutine != null)
        {
            StopCoroutine(invisibleCoroutine);
            invisibleCoroutine = StartCoroutine(Invisible(time));
        }
        else {
            invisibleCoroutine = StartCoroutine(Invisible(time));
        }

    }


    IEnumerator Invisible(float time)
    {
        isInvisible = true;

        if (playerRenderer != null)
        {
            playerRenderer.color = new Vector4(1f, 1f, 1f, 0.05f);
            GetComponentInChildren<TrailRenderer>().enabled = false;
        }
        if (playerAudio != null && invisibleClip != null) playerAudio.PlayOneShot(invisibleClip);
        yield return new WaitForSeconds(time);
        if (playerRenderer != null)
        {
            if(isStunned)
                playerRenderer.color = Color.red;
            else
                playerRenderer.color = new Vector4(1f,1f,1f,1f);
            GetComponentInChildren<TrailRenderer>().enabled = true;
        }
        isInvisible = false;
        invisibleCoroutine = null;
    }

    public void StunPlayer(float time)
    {
        isStunned = !isStunned;
        if (playerAudio != null && StunnedClip != null) playerAudio.PlayOneShot(StunnedClip);

        if (isStunned)
        {
            if (playerRenderer != null && !isInvisible) playerRenderer.color = Color.red;
        }
        else if (!isStunned)
        {
            if (playerRenderer != null && !isInvisible) playerRenderer.color = Color.white;
        }


        //if (stunCoroutine != null)
        //{
        //    StopCoroutine(stunCoroutine);
        //    paralizeCoroutine = StartCoroutine(Stunned(time));
        //}
        //else
        //{
        //    stunCoroutine = StartCoroutine(Stunned(time));
        //}
    }

    //IEnumerator Stunned(float time)
    //{
    //    isStunned = true;
    //    if (playerRenderer != null && !isInvisible) playerRenderer.color = Color.red;
    //    if (playerAudio != null && StunnedClip != null) playerAudio.PlayOneShot(StunnedClip);
    //    yield return new WaitForSeconds(time);
    //    if (playerRenderer != null && !isInvisible) playerRenderer.color = Color.white;
    //    isStunned = false;
    //}
    

    IEnumerator DeathRoutine()
    {
        if (playerAudio != null && deathClip != null) playerAudio.PlayOneShot(deathClip);

        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;

        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }

        yield return new WaitForSeconds(2.0f);
        //LevelManager.sharedInstance.ChangeScene("LevelSelector");
        GameManager.sharedInstance.GameOver();
    }

}
