using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballSpeed = 20.0f;
    private Rigidbody2D ballRigidBody;
    public Transform ballStartPos;

    bool isBallLaunched = false;

    Animator ballAnimator;

    Vector2 launchDirection = new Vector2 (0.1f, 0.9f).normalized;

    //Factor para ver que tan inclinada sale la pelota al choque con la paleta
    [SerializeField] float helpFactor = 0.5f;

    //Ayuda para que la pelota no baje muy lento o se quede atascada;
    [SerializeField] float stuckVelocity = 1.0f;
    [SerializeField] float unstuckFactor = 5.0f;
    float stuckDistance = 30.0f;


    //Sonido
    AudioSource ballAudio;

    //collider para no hacer daño antes de lanzar la bola
    Collider2D ballCollider;

    

    private void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
        ballAudio = GetComponent<AudioSource>();
        ballCollider = GetComponent<Collider2D>();
        ballAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        ResetLaunch();
    }

    private void Update()
    {
        if (!isBallLaunched)
        {
            ResetPos();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchBall();
            }

        }

        IsStuck();


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float x = hitFactor(this.transform.position, collision.transform.position, collision.collider.bounds.size.x);

            Vector2 direction = new Vector2(x, 1).normalized;
            ballRigidBody.velocity = direction * ballSpeed;
        }
    }

    float hitFactor(Vector2 ballPosition, Vector2 playerPos, float playerWidth)
    {
        return (ballPosition.x - playerPos.x)/(playerWidth * helpFactor);
    }


    void LaunchBall()
    {
        if (GameManager.sharedInstance.canThrowBall)
        {
            isBallLaunched = true;
            ballRigidBody.velocity = launchDirection * ballSpeed;
            ballCollider.enabled = true;
        }
    }

    public void ResetPos()
    {
        transform.position = ballStartPos.position;
    }

    public void ResetLaunch()
    {
        isBallLaunched = false;
        if (ballAnimator != null) ballAnimator.Play("Reappear_Ball");
        ResetPos();
        ballCollider.enabled = false;

    }

    void IsStuck()
    {
        if (ballRigidBody.velocity.y <= stuckVelocity && ballRigidBody.velocity.y >= -stuckVelocity)
        {
            Vector2 direction = new Vector2(ballRigidBody.velocity.x, unstuckFactor);
            ballRigidBody.velocity = direction * ballSpeed;
        }

        else if (ballRigidBody.velocity.magnitude != ballSpeed) // >= || ballRigidBody.velocity.magnitude <= -ballSpeed)
        {
            Vector2 direction = ballRigidBody.velocity.normalized;
            ballRigidBody.velocity = direction * ballSpeed;
        }

        else if (Vector2.Distance(this.gameObject.transform.position, Vector2.zero) >= stuckDistance)
        {
            ResetLaunch();
        }

        
    
    }

    public void PlayHitSound()
    {
        if (ballAudio != null)
        {
            ballAudio.Play();
        }
    }



}
