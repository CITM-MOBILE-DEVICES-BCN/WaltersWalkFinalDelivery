using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsControllerTest : MonoBehaviour
{
	private Animator animator;
	private bool isPhoneActive = false;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Q))
		{
			animator.SetBool("isPhoneActive", true);
			animator.SetTrigger("PhoneOut");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isPhoneActive", false);
            animator.SetTrigger("StorePhone");
        }

        if (Input.GetKeyDown(KeyCode.E))
		{
            animator.SetTrigger("Scratching");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
            animator.SetTrigger("Pills");
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
            animator.SetTrigger("SmokeCig");
		}
	}
}
