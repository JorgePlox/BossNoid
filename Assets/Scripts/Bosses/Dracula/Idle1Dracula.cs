using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle1Dracula : StateMachineBehaviour
{
    private int nextMovement;
    bool isFase2 = false;

    GameObject leftGun;
    GameObject rightGun;




    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftGun = GameObject.Find("L_Gun");
        rightGun = GameObject.Find("R_Gun");

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
                animator.SetTrigger("Fase1Attack1");
            }

            else if (nextMovement == 2)
            {
                animator.SetTrigger("Fase1Attack2");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    void EnterFase2(Animator anim)
    {
        if (!isFase2)
        {
            if (leftGun == null || rightGun == null)
            {
                anim.SetBool("Fase2", true);
                isFase2 = true;
            }
        }
    }
}
