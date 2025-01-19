using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class PopUpTest : MonoBehaviour
    {
        private PopUpSpawn popUp;

        private void Start()
        {
            popUp = GetComponent<PopUpSpawn>();
            popUp.SpawnAnim(); 
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.D))
            {
                popUp.DespawnPopUp();
            }
        }
    }
}
