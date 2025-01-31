﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyScript : MonoBehaviour
{
	public Animator Shady;
	
	public Animator Camera;
	
	public void OpenShop()
	{
		Shady.SetBool("OpenShop", true);
		
		Camera.SetBool("ShadySHop",true);
	}
	
	public void CloseShop()
	{
		Shady.SetBool("OpenShop", false);
		
		Camera.SetBool("ShadySHop",false);
		
	}
	
	public void GoOutside()
	{
		Camera.SetBool("GoOutside", true);
	}
	
	public void OpenCases()
	{
		Camera.SetBool("OpenCases", true);
	}
	
	public void CloseCases()
	{
		Camera.SetBool("OpenCases", false);
	}
}
