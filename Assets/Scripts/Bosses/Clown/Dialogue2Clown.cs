using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue2Clown : StateMachineBehaviour
{
    GameObject head;
    DialogueTrigger dialogue;

    AudioSource audioSource;
    [SerializeField] AudioClip secondSong;

    [SerializeField] float fadeTime;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
        GameManager.sharedInstance.ResetBall();
        head = GameObject.Find("Head");
        dialogue = head.GetComponent<DialogueTrigger>();
        dialogue.TriggerDialogue();


        if (!BGMManager.isFading && secondSong != null)
        {
            BGMManager.sharedInstance.StartCoroutine(BGMManager.sharedInstance.FadeSound(audioSource, fadeTime));
        }

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
        if (secondSong != null)
        BGMManager.sharedInstance.ChangeMusic(audioSource, secondSong);
    }
}
