using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class AcceptPhoneCall : MonoBehaviour
    {
        public GameObject declineButton;
       
        public void OnAccept()
        {
            StartCoroutine(Waiter());
        }

        IEnumerator Waiter()
        {
            yield return new WaitForSeconds(3);
            declineButton.SetActive(true);
            declineButton.transform.localPosition = new Vector3(0, -27.3f, 0);
            declineButton.transform.localScale = new Vector3(0.419999987f, 0.280000001f, 0.699999988f);
         
        }
    }
}
