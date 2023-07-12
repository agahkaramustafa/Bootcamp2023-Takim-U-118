using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyMaxHealth = 100;

    Animator anim;
    EnemyAI enemyAI;
    Collider m_collider;
    HealthBar healthBarScript;

    private float currentHealth;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        currentHealth = enemyMaxHealth;
        anim = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        m_collider = GetComponent<Collider>();
        healthBarScript = GetComponentInChildren<HealthBar>();

        healthBarScript.UpdateHealthBar(enemyMaxHealth, currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBarScript.UpdateHealthBar(enemyMaxHealth, currentHealth);
        anim.SetTrigger("Reaction");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        healthBarScript.gameObject.SetActive(false);
        enemyAI.enabled = false;
        m_collider.enabled = false;
    }
}
