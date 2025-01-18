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
        private float timeAlive = 8f;
        public float speed = 0f;

        public void Start()
        {
            if (dir == Direction.LEFT)
            {
                direction = new Vector3(1, 0, 0);
            }
            else
            {
                direction = new Vector3(-1, 0, 0);
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
