using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace WalterWalk
{
    public class PathFollower : MonoBehaviour
	{
		public float Speed;
    	
		private PathRetriever path;
		public Vector3 currentDestination = Vector3.zero;
		private Rigidbody rigid;

		async void Awake()
		{
			Speed *= 1000f;
			rigid = GetComponent<Rigidbody>();
			
			await Task.Delay(6000); // 6 seconds
            path = GetComponent<PathRetriever>();
            currentDestination = path.PopPoint();
			currentDestination.y = transform.position.y;
		}

        // Update is called once per frame
        void Update()
        {
			if (path != null)
			{
				if (Vector3.Distance(transform.position , currentDestination) <= 0.5f)
				{
                    currentDestination = path.PopPoint(); /* Get next destination */
					currentDestination.y = transform.position.y; // Lock y
                }
				Vector3 direction = (currentDestination - transform.position).normalized;
				rigid.velocity = direction * Speed * Time.deltaTime;
			}
        }
    }
}
