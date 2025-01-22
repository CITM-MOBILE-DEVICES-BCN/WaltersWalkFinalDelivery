using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyScript : MonoBehaviour
{
	public Animator Shady;
	
	public void OpenShop()
	{
		Shady.SetBool("OpenShop", true);
	}
}
