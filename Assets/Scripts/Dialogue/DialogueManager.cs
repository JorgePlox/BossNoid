using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static Queue<string> sentences;
    public bool isOnDialogue = false;

    public static DialogueManager sharedInstance;

    [SerializeField] Text dialogueText;
    [SerializeField] Canvas dialogueCanvas;


    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
        dialogueText.enabled = false;
        dialogueCanvas.enabled = false;
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        GameManager.sharedInstance.canThrowBall = false;
        isOnDialogue = true;
        dialogueText.enabled = true;
        dialogueCanvas.enabled = true;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        
       string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        GameManager.sharedInstance.canThrowBall = true;
        isOnDialogue = false;
        dialogueText.enabled = false;
        dialogueCanvas.enabled = false;
    }
}
