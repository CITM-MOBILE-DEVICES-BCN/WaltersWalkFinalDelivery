using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WalterWalk
{
    public class ExitMenu : MonoBehaviour
    {
        public GameObject walkerSpawner;
        public void ExitToMenu()
        {
            walkerSpawner.SetActive(false);
           StartCoroutine(LoadMenu());
        }

        IEnumerator LoadMenu()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Loading2");
        }
    }
}
