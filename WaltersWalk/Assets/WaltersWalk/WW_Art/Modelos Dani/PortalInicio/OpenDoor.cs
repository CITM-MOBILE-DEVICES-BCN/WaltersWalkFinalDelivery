using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
	public class OpenDoor : MonoBehaviour
	{
		public Animator Puerta;

		public void StartGame()
		{
			Puerta.SetBool("NewGame", true);
			if (PlayerManager.instance != null) { PlayerManager.instance.isDoorOpen = true; }
		}
	}

}