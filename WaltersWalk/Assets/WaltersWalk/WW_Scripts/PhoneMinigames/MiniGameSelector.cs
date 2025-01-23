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

        public GameObject dopamineBar;
        void Start()
        {
            memeWeightCalculator = new WeightedRandomSelector(minigamePrefabs.Length);
        }

        public void GetRandomMinigame()
        {
            if (gameObject.transform.childCount > 0 && currentMinigame != null)
            {
                Destroy(currentMinigame);
            }

            int index = memeWeightCalculator.GetNextWeightedRandom();
            currentMinigame = Instantiate(minigamePrefabs[index],gameObject.transform);
        }
    }
}
