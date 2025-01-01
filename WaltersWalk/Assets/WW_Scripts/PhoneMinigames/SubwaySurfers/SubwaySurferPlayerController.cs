using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class SubwaySurferPlayerController : MonoBehaviour
    {
        bool lockMovement = false;
        float speed = 0.2f;

        void Update()
        {
            if (!lockMovement)
            {
               HandleMovement();
            }
        }

        void HandleMovement()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                lockMovement = true;
                if (transform.localPosition.x == 0)
                {
                    transform.DOMoveX(-1.6f, speed).OnComplete(() => lockMovement = false);
                }
                else if (transform.localPosition.x != -1.6f)
                {
                    transform.DOMoveX(0, speed).OnComplete(() => lockMovement = false);
                }
                else
                {
                    lockMovement = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                lockMovement = true;
                if (transform.localPosition.x == 0)
                {
                    transform.DOMoveX(1.6f, speed).OnComplete(() => lockMovement = false);
                }
                else if (transform.localPosition.x != 1.6f)
                {
                    transform.DOMoveX(0, speed).OnComplete(() => lockMovement = false);
                }
                else
                {
                    lockMovement = false;
                }
            }
        } 
    }
}
