using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	public Animator Puerta;
 
	public void StartGame()
	{
		Puerta.SetBool("NewGame", true);
	}
}
