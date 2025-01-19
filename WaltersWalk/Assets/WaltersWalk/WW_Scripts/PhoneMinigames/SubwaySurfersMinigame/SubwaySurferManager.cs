using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class SubwaySurferManager : MonoBehaviour
    {
        public GameObject subwaySurferEnvironment;
        GameObject subwaySurferEnvironmentInstance;
        void Start()
        {
            subwaySurferEnvironmentInstance = Instantiate(subwaySurferEnvironment,new Vector3(0, -48.46f, -1.62f), Quaternion.identity);
        }

        private void OnDestroy()
        {
            Destroy(subwaySurferEnvironmentInstance);
        }
    }
}
