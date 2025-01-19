using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsControllerTest : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip pillsClip;
    public AudioClip smokingClip;
    public AudioClip scratchingClip;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsPhoneActive", true);
            animator.SetTrigger("PhoneOut");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("IsPhoneActive", false);
            animator.SetTrigger("StorePhone");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Scratching");
            audioSource.clip = scratchingClip;
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Pills");
            audioSource.clip = pillsClip;
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("SmokeCig");
            audioSource.clip = smokingClip;
            audioSource.Play();
        }
    }
}

