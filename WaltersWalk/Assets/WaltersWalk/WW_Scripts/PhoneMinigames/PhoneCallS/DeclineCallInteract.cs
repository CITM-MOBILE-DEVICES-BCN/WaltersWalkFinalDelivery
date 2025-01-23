using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class DeclineCallInteract : MonoBehaviour
    {
        public GameObject callParent;
        public PhoneCallAudio phoneCallAudio;
       
        public void ClickDecline()
        {
            phoneCallAudio.OnDeclineCall();
            Destroy(callParent);
            callParent.transform.parent.GetComponent<PhoneCallSpawner>().isPhoneCallActive = false;
        }
    }
}
