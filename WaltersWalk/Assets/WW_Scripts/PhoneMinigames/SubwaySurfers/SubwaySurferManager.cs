using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class SubwaySurferManager : MonoBehaviour
    {
        public GameObject subwaySurferEnvironment;
        void Start()
        {
            Instantiate(subwaySurferEnvironment,new Vector3(0, -48.46f, -1.62f), Quaternion.identity);
        }
    }
}
