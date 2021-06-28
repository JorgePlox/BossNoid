using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle3Clown : StateMachineBehaviour
{
    int nextMovement;
    bool isDialogue2 = false;

    GameObject chest;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chest = GameObject.Find("Body");
        nextMovement = Random.Range(1, 5);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnterDialogue2(animator);

        if (!isDialogue2)
        {

            if (nextMovement == 1)
            {
                animator.SetTrigger("Fase3Attack1");
            }

            else if (nextMovement == 2)
            {
                animator.SetTrigger("Fase3Attack2");
            }

            else if (nextMovement == 3)
            {
                animator.SetTrigger("Fase3Attack3");
            }

            else if (nextMovement == 4)
            {
                animator.SetTrigger("Fase3Attack4");
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    void EnterDialogue2(Animator anim)
    {
        if (!isDialogue2)
        {
            if (chest == null)
            {
                anim.SetBool("Dialogue2", true);
                isDialogue2 = true;
            }
        }
    }
}
