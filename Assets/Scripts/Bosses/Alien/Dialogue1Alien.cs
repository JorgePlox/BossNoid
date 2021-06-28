using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue1Alien : StateMachineBehaviour
{
    DialogueTrigger ship;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.sharedInstance.ResetBall();
        ship = GameObject.Find("Ship").GetComponent<DialogueTrigger>();
        ship.TriggerDialogue();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.sharedInstance.DisplayNextSentence();
        }

        if (!DialogueManager.sharedInstance.isOnDialogue)
        {
            animator.SetBool("Fase4", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
