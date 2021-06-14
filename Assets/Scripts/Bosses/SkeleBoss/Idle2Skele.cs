using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle2Skele : StateMachineBehaviour
{
    //2 es ataque, 1 es mantener idle
    private int nextMovement;

    GameObject leftArm;
    GameObject rightArm;

    bool isLeftNext = false;
    bool isRightNext = false;

    bool isFase3 = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftArm = GameObject.Find("L_Arm");
        rightArm = GameObject.Find("R_Arm");


        if (leftArm == null)
        {
            isRightNext = true;
        }

        else if (rightArm == null)
        {
            isLeftNext = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterFase3(animator);

        if (!isFase3)
        {

            if (isLeftNext)
            {
                animator.SetTrigger("LeftAttack2");
            }

            else if (isRightNext)
            {
                animator.SetTrigger("RightAttack2");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("LeftAttack2");
        animator.ResetTrigger("RightAttack2");
    }


    void EnterFase3(Animator anim)
    {
        if (!isFase3)
        {
            if (leftArm == null && rightArm == null)
            {
                anim.SetBool("Fase3", true);
                isFase3 = true;
            }
        }
    }

}
