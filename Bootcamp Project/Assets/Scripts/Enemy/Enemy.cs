using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerHitProcess playerHitProcessScript;
    private bool hitOneTime = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        playerHitProcessScript = FindObjectOfType<PlayerHitProcess>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerHitProcessScript.Hit1Activated || playerHitProcessScript.Hit2Activated)
        {
            if (other.CompareTag("Sword") && !hitOneTime)
            {
                // Deal damage
                Debug.Log("Enemy hit succesful");
                hitOneTime = true;
            }
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sword"))
            {
                // Deal damage
                Debug.Log("Enemy hit reset succesful");
                hitOneTime = false;
            }
    }

}
