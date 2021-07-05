using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDracula : StateMachineBehaviour
{
    GameObject head;
    CameraShake camera;

    float time = 0.0f;
    bool hasPlayedParticle = false;
    bool isOver = false;

    [SerializeField] float newShakeDuration = 4.0f;
    [SerializeField] float newShakeAmount = 0.5f;

    [SerializeField] float endTime = 9.0f;
    [SerializeField] float particleStartTime = 3.0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.sharedInstance.canThrowBall = false;
        GameManager.sharedInstance.ResetBall();
        head = GameObject.Find("Head");
        camera = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        camera.enabled = true;
        camera.shakeDuration = newShakeDuration;
        camera.shakeAmount = newShakeAmount;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time >= endTime && !isOver)
        {
            head.GetComponent<Block>().KillBoss();
            isOver = true;
        }

        if (time >= particleStartTime && !hasPlayedParticle)
        {
            head.GetComponent<Block>().PlayFinalParticle();
            hasPlayedParticle = true;
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
