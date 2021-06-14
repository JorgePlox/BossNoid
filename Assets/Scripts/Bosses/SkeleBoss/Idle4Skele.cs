using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle4Skele : StateMachineBehaviour
{
    int nextMovement;
    bool isFase5 = false;

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
        EnterFase5(animator);

        if (!isFase5)
        {

            if (nextMovement == 1)
            {
                animator.SetTrigger("Chest1");
            }

            else if (nextMovement == 2)
            {
                animator.SetTrigger("Chest2");
            }

            else if (nextMovement == 3)
            {
                animator.SetTrigger("Chest3");
            }

            else if (nextMovement == 4)
            {
                animator.SetTrigger("Chest4");
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    void EnterFase5(Animator anim)
    {
        if (!isFase5)
        {
            if (chest == null)
            {
                anim.SetBool("Fase5", true);
                isFase5 = true;
            }
        }
    }


}
