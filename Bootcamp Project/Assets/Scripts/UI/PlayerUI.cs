using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private Image manaBarSprite;

    public bool HasMana { get { return hasMana; } }
    private bool hasMana = true;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateManaBar(float maxActivationTime, float currentActivationTime)
    {
        StartCoroutine(ManaRoutine(maxActivationTime, currentActivationTime));
    }

    IEnumerator ManaRoutine(float maxActivationTime, float currentActivationTime)
    {
        while (currentActivationTime / maxActivationTime >= Mathf.Epsilon)
        {
            manaBarSprite.fillAmount = currentActivationTime / maxActivationTime;
            currentActivationTime -= Time.deltaTime;
            yield return null;
        }

        manaBarSprite.fillAmount = 0f / maxActivationTime;
        hasMana = false;
    }
}
