using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PhoneMinigames
{
    public class WarningPopUp : MonoBehaviour
    {
        public float appearTime = 0.25f;
        public float disappearTime = 0.1f;
        void Start()
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(0, 0, 0);
            transform.DOScale(new Vector3(1, 1, 1), appearTime);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public void Close()
        {
            transform.DOScale(new Vector3(0, 0, 0), disappearTime).OnComplete(() => Destroy(gameObject));
        }

    }
}
