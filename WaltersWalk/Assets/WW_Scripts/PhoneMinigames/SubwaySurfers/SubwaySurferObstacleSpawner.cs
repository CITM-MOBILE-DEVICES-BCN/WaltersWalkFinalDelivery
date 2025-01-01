using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalterWalk;

namespace PhoneMinigames
{
    public class SubwaySurferObstacleSpawner : MonoBehaviour
    {

        public GameObject[] obstaclesPrefab;
        WeightedRandomSelector obstacleWeightCalculator;
        public float interval = 1.5f;
        void Start()
        {
            obstacleWeightCalculator = new WeightedRandomSelector(obstaclesPrefab.Length);
            InvokeRepeating("GetRandomObstacle", 0, interval);
        }

        public void GetRandomObstacle()
        {
            int index = obstacleWeightCalculator.GetNextWeightedRandom();
            Instantiate(obstaclesPrefab[index], transform);
        }
    }
}
