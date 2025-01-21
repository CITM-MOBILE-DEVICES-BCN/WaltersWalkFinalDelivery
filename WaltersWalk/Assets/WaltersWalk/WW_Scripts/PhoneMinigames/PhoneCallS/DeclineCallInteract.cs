using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class DeclineCallInteract : MonoBehaviour
    {
        public GameObject callParent;
        public void ClickDecline()
        {
            Destroy(callParent);
        }
    }
}
