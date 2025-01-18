using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WalterWalk
{
    public class PlayerCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "car")
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
