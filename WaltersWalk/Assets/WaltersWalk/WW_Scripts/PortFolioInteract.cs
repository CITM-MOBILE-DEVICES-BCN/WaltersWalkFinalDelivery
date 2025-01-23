using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WalterWalk
{
    public class PortFolioInteract : MonoBehaviour
    {
       public void OnPortFolioClick()
       {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
