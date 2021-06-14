using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle1Skele : StateMachineBehaviour
{
    private int nextMovement;

    GameObject leftArm;
    GameObject rightArm;

    bool isFase2 = false;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        leftArm = GameObject.Find("L_Arm");
        rightArm = GameObject.Find("R_Arm");
        nextMovement = Random.Range(1, 3);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterFase2(animator);

        if (!isFase2)
        {
            if (nextMovement == 1)
            {
                animator.SetTrigger("LeftAttack1");
            }

            else if (nextMovement == 2)
            {
                animator.SetTrigger("RightAttack1");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("LeftAttack1");
        animator.ResetTrigger("RightAttack1");
    }

    void EnterFase2(Animator anim)
    {
        if (!isFase2)
        {
            if (leftArm == null || rightArm == null)
            {
                anim.SetBool("Fase2", true);
                isFase2 = true;
            }
        }
    }

}
