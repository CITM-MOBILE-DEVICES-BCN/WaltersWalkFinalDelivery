using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalterWalk;

namespace PhoneMinigames
{
    public class PhoneCallSpawner : MonoBehaviour
    {

        public GameObject[] phoneCallPrefabs;
        WeightedRandomSelector phonecallRandomSelector;
        public bool isPhoneCallActive = false;

        private void Start()
        {
            phonecallRandomSelector = new WeightedRandomSelector(phoneCallPrefabs.Length);
        }

        void Update()
        {
            if (GlobalVariables.isAirPlaneModeActive)
            {
                return;
            }
            //if in a random time between 0 and 1 , spawn a phone call prefab
            if (Random.value < 0.003f && isPhoneCallActive == false && PlayerManager.instance.isDoorOpen)
            {
                GameObject phoneCallPrefab = phoneCallPrefabs[phonecallRandomSelector.GetNextWeightedRandom()];
                GameObject phoneCall = Instantiate(phoneCallPrefab, transform.position, Quaternion.Euler(0,-180,0),transform);
                phoneCall.transform.localEulerAngles = new Vector3(0, 0, 0);
                phoneCall.transform.localPosition = new Vector3(0, 0, -0.54f);
                phoneCall.transform.localScale = new Vector3(phoneCall.transform.localScale.x , phoneCall.transform.localScale.y, phoneCall.transform.localScale.z);
                isPhoneCallActive = true;
            }
        }
    }
}
