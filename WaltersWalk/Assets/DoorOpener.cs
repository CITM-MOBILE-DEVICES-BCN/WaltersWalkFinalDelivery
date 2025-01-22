using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class DoorOpener : MonoBehaviour
    {

        public Animator Door;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject clickedObject = hit.collider.gameObject;


                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Door.SetBool("NewGame", true);
                        if (PlayerManager.instance != null) { PlayerManager.instance.isDoorOpen = true; }
                    }
                }
            }
        }
    }
}