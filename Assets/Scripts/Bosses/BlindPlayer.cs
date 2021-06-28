using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindPlayer : MonoBehaviour
{


    private void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.sharedInstance.TurnInvisible(2.0f);
        }
    }


}