using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerHitProcess playerHitProcessScript;
    private EnemyHealth enemyHealthScript;
    private bool allowHit = true;
    private int playerDamage = 5;

    private WaitForSeconds takenDamageWait = new WaitForSeconds(.8f);

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        playerHitProcessScript = FindObjectOfType<PlayerHitProcess>();
        enemyHealthScript = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerHitProcessScript.Hit1Activated || playerHitProcessScript.Hit2Activated)
        {
            if (other.CompareTag("Sword") && allowHit)
            {
                enemyHealthScript.TakeDamage(playerDamage);
                StartCoroutine(nameof(ToggleAllowHitRoutine));
                Debug.Log("Enemy hit succesful");
            }
        }
    }

    IEnumerator ToggleAllowHitRoutine()
    {
        allowHit = false;
        yield return takenDamageWait;
        allowHit = true;
    }

}
