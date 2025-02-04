using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public Collider swordCollider; // Drag and drop the sword's collider here in the inspector
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (swordCollider != null)
        {
            swordCollider.enabled = false; // Ensure the sword collider is disabled at the start
        }
    }

    void Update()
    {
        // Reset combo if too much time has passed
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        // Cooldown time check
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0)) // Left-click for attack
            {
                OnClick();
            }
        }

        // Handle combo attack states
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;

        if (noOfClicks == 1)
        {
            anim.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }
    }

    // Enable sword collider during attack
    public void EnableSword()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = true;
        }
    }

    // Disable sword collider after attack
    public void DisableSword()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }
}
