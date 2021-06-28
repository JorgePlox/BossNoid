using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle3Alien : StateMachineBehaviour
{

    GameObject leftMinion;
    GameObject centerMinion;
    GameObject rightMinion;

    bool isDialogue1 = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftMinion = GameObject.Find("Minion1");
        centerMinion = GameObject.Find("Minion2");
        rightMinion = GameObject.Find("Minion3");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterFase4(animator);

        if (!isDialogue1)
        {

            animator.SetTrigger("Fase3Attack1");
    
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Fase3Attack1");
    }


    void EnterFase4(Animator anim)
    {
        if (!isDialogue1)
        {
            if ((leftMinion == null && centerMinion == null && rightMinion == null))
            {
                anim.SetBool("Dialogue1", true);
                isDialogue1 = true;
            }
        }
    }


}
