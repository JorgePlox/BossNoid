using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle2Clown : StateMachineBehaviour
{
    private int nextMovement;

    GameObject leftFoot;
    GameObject rightFoot;

    bool isLeftNext = false;
    bool isRightNext = false;

    bool isFase3 = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftFoot = GameObject.Find("L_Foot");
        rightFoot = GameObject.Find("R_Foot");


        if (leftFoot == null)
        {
            isRightNext = true;
        }

        else if (rightFoot == null)
        {
            isLeftNext = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterDialogue1(animator);

        if (!isFase3)
        {

            if (isLeftNext)
            {
                animator.SetTrigger("Fase2Attack2");
            }

            else if (isRightNext)
            {
                animator.SetTrigger("Fase2Attack1");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    void EnterDialogue1(Animator anim)
    {
        if (!isFase3)
        {
            if (leftFoot == null && rightFoot == null)
            {
                anim.SetBool("Dialogue1", true);
                isFase3 = true;
            }
        }
    }
}
