using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsControllerTest : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("IsPhoneActive", true);
            animator.SetTrigger("PhoneOut");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("IsPhoneActive", false);
            animator.SetTrigger("StorePhone");
        }

    }
}

