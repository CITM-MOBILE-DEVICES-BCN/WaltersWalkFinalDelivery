using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class AcceptCallInteract : MonoBehaviour
    {
        public GameObject declineButton;
        public GameObject phoneCall;
        public void AcceptCall()
        {
            phoneCall.GetComponent<AcceptPhoneCall>().OnAccept();
            declineButton.SetActive(false);
            Destroy(gameObject);

        }
    }
}
