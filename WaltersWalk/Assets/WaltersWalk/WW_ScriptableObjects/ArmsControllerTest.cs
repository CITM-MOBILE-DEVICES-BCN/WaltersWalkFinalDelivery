using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsControllerTest : MonoBehaviour
{
    private Animator animator;
    public bool isPhoneOut = false;

    [SerializeField] GameObject moveCameraTracker;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isPhoneOut == false)
        {
            animator.SetBool("IsPhoneActive", true);
            animator.SetTrigger("PhoneOut");
            isPhoneOut = true;
            moveCameraTracker.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.S) && isPhoneOut ==  true)
        {
            animator.SetBool("IsPhoneActive", false);
            animator.SetTrigger("StorePhone");
            isPhoneOut = false;
            moveCameraTracker.SetActive(true);
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    animator.SetTrigger("Scratching");
        //}
    }
}

