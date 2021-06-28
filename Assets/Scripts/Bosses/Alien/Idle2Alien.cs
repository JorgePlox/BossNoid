using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle2Alien : StateMachineBehaviour
{
    
    private int nextMovement;

    GameObject leftMinion;
    GameObject centerMinion;
    GameObject rightMinion;

    bool isLeftNull = false;
    bool isCenterNull = false;
    bool isRightNull = false;

    bool isFase3 = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftMinion = GameObject.Find("Minion1");
        centerMinion = GameObject.Find("Minion2");
        rightMinion = GameObject.Find("Minion3");


        if (leftMinion == null)
        {
            isLeftNull = true;
        }

        else if (centerMinion == null)
        {
            isCenterNull = true;
        }

        else if (rightMinion == null)
        {
            isRightNull = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterFase3(animator);

        if (!isFase3)
        {

            if (isLeftNull)
            {
                animator.SetTrigger("Fase2Attack1");
            }

            else if (isCenterNull)
            {
                animator.SetTrigger("Fase2Attack2");
            }

            else if (isRightNull)
            {
                animator.SetTrigger("Fase2Attack3");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Fase2Attack1");
        animator.ResetTrigger("Fase2Attack2");
        animator.ResetTrigger("Fase2Attack3");
    }


    void EnterFase3(Animator anim)
    {
        if (!isFase3)
        {
            if ((leftMinion == null && centerMinion == null) || (centerMinion == null && rightMinion == null) || (leftMinion == null && rightMinion == null))
            {
                anim.SetBool("Fase3", true);
                isFase3 = true;
            }
        }
    }
}
