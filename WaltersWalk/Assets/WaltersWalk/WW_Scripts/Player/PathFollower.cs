using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace WalterWalk
{
	
	public enum Orientation
	{
		Horizontal,
		Vertical
	}
	
    public class PathFollower : MonoBehaviour
	{
		public float Speed;
    	
		private PathRetriever path;
		public Vector3 currentDestination = Vector3.zero;
		private Rigidbody rigid;
		
		public Orientation orientation = Orientation.Vertical;

		bool stoped = false;

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

			if (Input.GetKeyDown(KeyCode.Space) == true && ! stoped)
			{
				stoped = true;
			}
			else if (Input.GetKeyDown(KeyCode.Space) == true && stoped)
			{
				stoped = false;
			}
			
			if (stoped)
			{
				rigid.velocity = Vector3.Lerp(rigid.velocity, Vector3.zero, .1f);

                return;
			}

			if (path != null)
			{
				if (Vector3.Distance(transform.position , currentDestination) <= 0.1f)
				{
                    currentDestination = path.PopPoint(); /* Get next destination */
					currentDestination.y = transform.position.y; // Lock y
					
					if(Mathf.Abs(currentDestination.x - transform.position.x) > 5)
					{
						orientation = Orientation.Horizontal;

					}
					else
					{
						orientation = Orientation.Vertical;
					}
					PlayerManager.instance.playerOrientation = orientation;
					
                }
				Vector3 direction = (currentDestination - transform.position).normalized;
				rigid.velocity = direction * Speed * Time.deltaTime;
			}
        }
    }
}
