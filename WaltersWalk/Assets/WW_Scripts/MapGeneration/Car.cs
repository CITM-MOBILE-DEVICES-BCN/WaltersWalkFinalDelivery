using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public class Car : MonoBehaviour
    {

        public Direction dir;
        public Vector3 direction;
        private float timeAlive = 6f;
        public float speed = 0f;

        private GameObject carSpawned = null;

        public void Start()
        {
          
            
	        GameObject[] cars = Resources.LoadAll<GameObject>("Cars");

            if (carSpawned == null)
            {
                carSpawned = Instantiate(cars[Random.Range(0, cars.Length - 1)], this.transform);
                carSpawned.transform.localPosition = new Vector3(0, -1, 0);
            }
	        if (dir == Direction.LEFT)
	        {
		        direction = new Vector3(1, 0, 0);
		        carSpawned.transform.eulerAngles = new Vector3(0,90,0);
	        }
	        else
	        {
		        direction = new Vector3(-1, 0, 0);
		        carSpawned.transform.eulerAngles = new Vector3(0,-90,0);
	        }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += direction * Time.deltaTime * speed;
        }

        private void OnEnable()
        {
            Invoke("Deactivate", timeAlive);
            Start();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
