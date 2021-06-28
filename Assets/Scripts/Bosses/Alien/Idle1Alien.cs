using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle1Alien : StateMachineBehaviour
{   
    private int nextMovement;
    bool isFase2 = false;

    GameObject leftMinion;
    GameObject centerMinion;
    GameObject rightMinion;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftMinion = GameObject.Find("Minion1");
        centerMinion = GameObject.Find("Minion2");
        rightMinion = GameObject.Find("Minion3");

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
                animator.SetTrigger("Attack1");
            }

            else if (nextMovement == 2)
            {
                animator.SetTrigger("Attack2");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
    }


    void EnterFase2(Animator anim)
    {
        if (!isFase2)
        {
            if (leftMinion == null || centerMinion == null || rightMinion ==null)
            {
                anim.SetBool("Fase2", true);
                isFase2 = true;
            }
        }
    }


}
