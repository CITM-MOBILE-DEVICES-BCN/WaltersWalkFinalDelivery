using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class CarGenerator : MonoBehaviour
    {
        int numCars = 3;
        public Direction dir;
        public float carSpeed = 6f;
        public Car carPref;
        public List<Car> cars = new List<Car>();

        private float spawnTime;
	    private int currentIndex = 0;
        
	    public bool goesDown = false;

        // Start is called before the first frame update
        void Start()
        {
            spawnTime = Random.Range(3f, 5f) + Random.Range(3f, 5f) -1f;

            for (int i = 0; i < numCars; i++)
            {
                Car spawned = Instantiate(carPref, this.transform);
                spawned.gameObject.transform.position = transform.position;
                spawned.gameObject.SetActive(false);
                spawned.speed = carSpeed;
	            spawned.dir = dir;
	            spawned.goesDown = goesDown;

                switch (i % 3)
                {
                    case 0:
                        spawned.claxonType = SoundType.CLAXON1;
                        break;
                    case 1:
                        spawned.claxonType = SoundType.CLAXON2;
                        break;
                    case 2:
                        spawned.claxonType = SoundType.CLAXON3;
                        break;
                }

                cars.Add(spawned);
            }

            InvokeRepeating("SpawnCar", spawnTime, spawnTime);
        }

        private void SpawnCar()
        {
            currentIndex++;
            cars[currentIndex % cars.Count].gameObject.SetActive(true);
            cars[currentIndex % cars.Count].transform.position = transform.position;
        }
    }
}
