using System.Collections;
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
}
