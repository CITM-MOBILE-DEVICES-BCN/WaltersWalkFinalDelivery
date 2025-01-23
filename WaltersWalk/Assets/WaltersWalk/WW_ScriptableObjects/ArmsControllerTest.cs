using PhoneMinigames;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmsControllerTest : MonoBehaviour
{
    private Animator animator;
    public MiniGameSelector gameSelector;
    public GameObject cameraMover;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("IsPhoneActive", true);
            animator.SetTrigger("PhoneOut");
            gameSelector.GetRandomMinigame();
            cameraMover.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("IsPhoneActive", false);
            animator.SetTrigger("StorePhone");
            cameraMover.SetActive(true);
        }

    }
}

