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

        void Start()
        {
            memeWeightCalculator = new WeightedRandomSelector(minigamePrefabs.Length);
            GetRandomMinigame();
        }

        public void GetRandomMinigame()
        {
            int index = memeWeightCalculator.GetNextWeightedRandom();
            Instantiate(minigamePrefabs[index],gameObject.transform);
        }

    }
}
