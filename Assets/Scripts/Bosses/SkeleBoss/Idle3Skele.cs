using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle3Skele : StateMachineBehaviour
{
    DialogueTrigger skeleton;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.sharedInstance.ResetBall();
        skeleton = GameObject.Find("SkeleBoss").GetComponent<DialogueTrigger>();
        skeleton.TriggerDialogue();

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
