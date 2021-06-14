using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalizePlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.sharedInstance.ParalizePlayer(1.0f);
        }
    }
}
