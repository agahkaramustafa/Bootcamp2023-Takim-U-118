using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitProcess : MonoBehaviour
{
    public bool Hit1Activated { get { return hit1Activated; } }
    private bool hit1Activated = false;

    public bool Hit2Activated { get { return hit2Activated; } }
    private bool hit2Activated = false;

    private float nextFireTime = 0f;
    private int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;

    private Animator anim;
    private Player playerScript;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        HitProcess();
    }

    private void HitProcess()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
            hit1Activated = anim.GetBool("Hit1");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            anim.SetBool("Hit2", false);
            hit2Activated = anim.GetBool("Hit2");
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0) && playerScript.HasPowerUp)
            {
                OnClick();
            }
        }
    }

    private void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;

        if (noOfClicks == 1)
        {
            anim.SetBool("Hit1", true);
            hit1Activated = anim.GetBool("Hit1");
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
            hit1Activated = anim.GetBool("Hit1");
            anim.SetBool("Hit2", true);
            hit2Activated = anim.GetBool("Hit2");
        }
    }
}
