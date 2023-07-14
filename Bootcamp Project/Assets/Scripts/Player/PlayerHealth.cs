using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerMaxHealth = 100;

    private float currentHealth;
    Animator anim;
    PlayerUI playerUIScript;

    // Start is called before the first frame update
    void Start()
    {
        playerUIScript = FindObjectOfType<PlayerUI>();
        anim = GetComponent<Animator>();
        currentHealth = playerMaxHealth;
        playerUIScript.UpdateHealthBar(playerMaxHealth, currentHealth);        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerUIScript.UpdateHealthBar(playerMaxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            // deathhandler script
            playerUIScript.gameObject.SetActive(false);
            anim.SetTrigger("IsDead");
            Debug.Log("Player is PEPSI");
        }
    }
}
