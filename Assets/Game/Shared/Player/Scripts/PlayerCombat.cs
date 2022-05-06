using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;

    private StarterAssetsInputs inputs;

    public float cooldownTime = 2f;
    private float nextFireTime = 0;
    public int noOfClicks = 0;

    float lastAttackTime = 0;
    float maxComboDelay = 1f;


    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f)
        {
            animator.SetBool("Hit1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            animator.SetBool("Hit2", false);
            noOfClicks = 0;
        }

        if (Time.time - lastAttackTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (inputs.rightAttack)
            {
                OnClick();
                inputs.rightAttack = false;
            }
        }
    }
    void OnClick()
    {
        lastAttackTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            animator.SetBool("Hit1", true);
        }

        noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            animator.SetBool("Hit1", false);
            animator.SetBool("Hit2", true);
        }
    }
}
