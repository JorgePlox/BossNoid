using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle4Alien : StateMachineBehaviour
{
    private int nextMovement;
    GameObject ship;

    bool isDialogue2 = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ship = GameObject.Find("Ship");
        nextMovement = Random.Range(1, 4);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterFase4(animator);

        if (!isDialogue2)
        {

            if (nextMovement == 1)
            {
                animator.SetTrigger("Fase4Attack1");
            }

            else if (nextMovement == 2)
            {
                animator.SetTrigger("Fase4Attack2");
            }

            else if (nextMovement == 3)
            {
                animator.SetTrigger("Fase4Attack3");
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Fase4Attack1");
        animator.ResetTrigger("Fase4Attack2");
        animator.ResetTrigger("Fase4Attack3");
    }


    void EnterFase4(Animator anim)
    {
        if (!isDialogue2)
        {
            if (ship == null)
            {
                anim.SetBool("Dialogue2", true);
                isDialogue2 = true;
            }
        }
    }


}
