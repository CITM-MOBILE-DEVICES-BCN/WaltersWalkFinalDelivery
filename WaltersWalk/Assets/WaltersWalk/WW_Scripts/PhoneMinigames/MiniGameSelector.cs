using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalterWalk;

namespace PhoneMinigames
{
    public class MiniGameSelector : MonoBehaviour
    {
        public GameObject[] minigamePrefabs;
        WeightedRandomSelector memeWeightCalculator;

        GameObject currentMinigame;

        void Start()
        {
            memeWeightCalculator = new WeightedRandomSelector(minigamePrefabs.Length);
        }

        public void GetRandomMinigame()
        {
            int index = memeWeightCalculator.GetNextWeightedRandom();
            currentMinigame = Instantiate(minigamePrefabs[index],gameObject.transform);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameObject.transform.childCount > 0)
                {
                    Destroy(currentMinigame);
                }


                GetRandomMinigame();
            }
        }

    }
}
