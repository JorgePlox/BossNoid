using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle2Dracula : StateMachineBehaviour
{

    GameObject leftGun;
    GameObject rightGun;

    bool isDialogue1 = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftGun = GameObject.Find("L_Gun");
        rightGun = GameObject.Find("R_Gun");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterDialogue1(animator);

        if (!isDialogue1)
        {

            animator.SetTrigger("Fase2Attack1");

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    void EnterDialogue1(Animator anim)
    {
        if (!isDialogue1)
        {
            if (leftGun == null && rightGun == null)
            {
                anim.SetBool("Dialogue1", true);
                isDialogue1 = true;
            }
        }
    }
}
