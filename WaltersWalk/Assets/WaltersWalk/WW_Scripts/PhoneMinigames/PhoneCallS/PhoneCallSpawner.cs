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
        bool isPhoneCallActive = false;
        public bool isAirPlaneModeActive = false;

        private void Start()
        {
            phonecallRandomSelector = new WeightedRandomSelector(phoneCallPrefabs.Length);
        }

        void Update()
        {
            if (isAirPlaneModeActive)
            {
                return;
            }
            //if in a random time between 0 and 1 , spawn a phone call prefab
            if (Random.value < 0.01f && isPhoneCallActive == false)
            {
                GameObject phoneCallPrefab = phoneCallPrefabs[phonecallRandomSelector.GetNextWeightedRandom()];
                GameObject phoneCall = Instantiate(phoneCallPrefab, transform.position, Quaternion.identity,transform);
                isPhoneCallActive = true;
            }
        }
    }
}
