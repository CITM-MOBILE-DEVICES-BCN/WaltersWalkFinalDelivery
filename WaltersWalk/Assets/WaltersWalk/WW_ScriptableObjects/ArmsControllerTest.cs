using PhoneMinigames;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WalterWalk;

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
        if (Input.GetKeyDown(KeyCode.W) && PlayerManager.instance.isDoorOpen)
        {
            animator.SetBool("IsPhoneActive", true);
            animator.SetTrigger("PhoneOut");
            gameSelector.GetRandomMinigame();
            cameraMover.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.S) && PlayerManager.instance.isDoorOpen)
        {
            animator.SetBool("IsPhoneActive", false);
            animator.SetTrigger("StorePhone");
            gameSelector.ClosePhone();
            cameraMover.SetActive(true);
        }

    }
}

