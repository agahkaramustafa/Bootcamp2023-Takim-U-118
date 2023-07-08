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

    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;


    private WaitForSeconds powerUpWaitTime = new WaitForSeconds(5);
    private Animator anim;

    private bool hasPowerUp;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            anim.SetBool("Hit2", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0) && hasPowerUp)
            {
                OnClick();
            }
        }

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

    private void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;

        if (noOfClicks == 1)
        {
            anim.SetBool("Hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
            anim.SetBool("Hit2", true);
        }

    }
}
