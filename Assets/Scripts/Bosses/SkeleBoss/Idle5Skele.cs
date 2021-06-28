using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle5Skele : StateMachineBehaviour
{
    int nextMovement;


    GameObject head;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        head = GameObject.Find("Head");
        nextMovement = Random.Range(1, 4);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (head.GetComponent<Block>().isDead)
        {
            animator.SetBool("Dead", true);
        }

        if (nextMovement == 1)
        {
            animator.SetTrigger("Head1");
        }

        else if (nextMovement == 2)
        {
            animator.SetTrigger("Head2");
        }

        else if (nextMovement == 3)
        {
            animator.SetTrigger("Head3");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
