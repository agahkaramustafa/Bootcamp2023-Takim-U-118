using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DissolveExample;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem powerUpParticleSystem;
    [SerializeField] GameObject horusSwordPrefab;
    [SerializeField] DissolveChilds dissolveChilds;

    private PlayerUI playerUIScript;
    private WaitForSeconds powerUpWaitTime = new WaitForSeconds(5);

    public bool HasPowerUp { get { return hasPowerUp; } }
    private bool hasPowerUp;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        playerUIScript = FindObjectOfType<PlayerUI>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && playerUIScript.HasMana)
        {
            PowerUpProcess();
        }
    }

    private void PowerUpProcess()
    {
        StartCoroutine(nameof(PowerUpRoutine));
    }

    IEnumerator PowerUpRoutine()
    {
        hasPowerUp = true;
        powerUpParticleSystem.Play();
        horusSwordPrefab.SetActive(hasPowerUp);
        dissolveChilds.Respawn();
        playerUIScript.UpdateManaBar(5f, 5f);
        yield return powerUpWaitTime;
        dissolveChilds.Dissolve();
        yield return new WaitForSeconds(2f);
        powerUpParticleSystem.Stop();
        horusSwordPrefab.SetActive(!hasPowerUp);
        hasPowerUp = false;
    }
}
