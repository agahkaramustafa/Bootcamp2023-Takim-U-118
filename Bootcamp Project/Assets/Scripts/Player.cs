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

    private WaitForSeconds powerUpWaitTime = new WaitForSeconds(5);

    private bool hasPowerUp;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
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
        yield return powerUpWaitTime;
        dissolveChilds.Dissolve();
        yield return new WaitForSeconds(2f);
        powerUpParticleSystem.Stop();
        horusSwordPrefab.SetActive(!hasPowerUp);
        hasPowerUp = false;
    }
}
