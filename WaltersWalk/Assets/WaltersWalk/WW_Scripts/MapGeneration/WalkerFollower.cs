using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class WalkerFollower : MonoBehaviour
    {


        public Queue<Vector3> points;
        public float speed = 5f;
        private Rigidbody rigid;

        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            points = new Queue<Vector3>();
        }

        // Update is called once per frame
        void Update()
        {
            if (points.Count == 0) { return; }
            // La posicion que recibe no es la que toca, 


            // haz el player movement, que pueda mirar siempre A , D y swipe
            // + una accion predeterminada de pararse
            Vector3 current = points.Peek();

            //if (Vector3.Distance(current, transform.position) > 15f)
            //{
                
            //    //Vector3 direction = current - transform.position;
            //    //rigid.velocity = direction.normalized * speed;
            //}
            //else
            //{
            //    transform.position = current;
            //    points.Dequeue();
            //}

        }

        public void AddPoint(Vector3 point)
        {
            points.Enqueue(new Vector3(point.x, transform.position.y, point.y));
            var i = points.Peek();
            transform.DOMove(points.Dequeue(), 1f, true).OnComplete(() => points.Dequeue());

        }

        public void ConnectToGenerator(WalkerCreator generator)
        {
            generator.OnTurnPlaced += AddPoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "building")
            {
                Destroy(other.gameObject);
            }
        }

    }
}
