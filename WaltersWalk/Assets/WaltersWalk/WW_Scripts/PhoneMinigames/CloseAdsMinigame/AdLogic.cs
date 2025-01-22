using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WalterWalk;

namespace PhoneMinigames
{
    public class AdLogic : MonoBehaviour
    {
        public Image adImg;

        public float appearTime = 0.75f;
        public float disappearTime = 0.5f;

        public GameObject warningPopup;
        void Start()
        {
            transform.localScale = new Vector3(0, 0, 0);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 500);
            transform.DOScale(new Vector3(0.479999989f, 0.239999995f, 0.200000003f), appearTime);
            transform.localRotation = Quaternion.Euler(0,0,0);
        }

        public void OnClickX()
        {
            transform.DOScale(new Vector3(0, 0, 0), disappearTime).OnComplete(() => Destroy(gameObject));
            DopamineBar.instance.DopaMineLevelValueModification(10);
        }

        public void OnClickAd()
        {
            Instantiate(warningPopup, transform.position, Quaternion.identity,transform.parent.parent);
            DopamineBar.instance.DopaMineLevelValueModification(-10);
        }

    }
}
