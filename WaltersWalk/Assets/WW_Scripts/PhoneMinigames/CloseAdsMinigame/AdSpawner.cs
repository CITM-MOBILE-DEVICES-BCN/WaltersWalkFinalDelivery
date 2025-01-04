using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalterWalk;

namespace PhoneMinigames
{
    public class AdSpawner : MonoBehaviour
    {

        public Vector2 minBounds;
        public Vector2 maxBounds;

        public GameObject adPrefab;
        WeightedRandomSelector adSpriteRandomSelector;
        
        public Sprite[] adSprites;

        public float spawnRate = 0.8f;

        [SerializeField]
        Transform adSpawnEnv;

        void Start()
        {
            adSpriteRandomSelector = new WeightedRandomSelector(adSprites.Length);
            InvokeRepeating("SpawnAd", 0, spawnRate); 
        }

        void SpawnAd()
        {
            float randomX = Random.Range(minBounds.x, maxBounds.x);
            float randomY = Random.Range(minBounds.y, maxBounds.y);
            
            GameObject adInstance = Instantiate(adPrefab, transform.position, Quaternion.identity,adSpawnEnv);
            adInstance.transform.localPosition = new Vector3(randomX, randomY, transform.position.z); 
            adInstance.GetComponent<AdLogic>().adImg.sprite = adSprites[adSpriteRandomSelector.GetNextWeightedRandom()];
        }

    }
}
