using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

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
		public PlayerCamera cam;

		public Vector3 currentDestination = Vector3.zero;
		private Rigidbody rigid;

		public Orientation orientation;
		
		bool stoped = false;

		bool left = false;
		bool first = true;

		async void Awake()
		{
			Speed *= 1000f;
			rigid = GetComponent<Rigidbody>();
			
			await Task.Delay(6000); // 6 seconds
            path = GetComponent<PathRetriever>();
            currentDestination = path.PopPoint();
			currentDestination.y = transform.position.y;
			orientation = Orientation.Vertical;
			PlayerManager.instance.playerOrientation = orientation;
		}

        // Update is called once per frame
        void Update()
        {

	        if (PlayerManager.instance.isDoorOpen){
		        Movement();
			
	        }
        }
        
        
		void Movement()
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
                Debug.Log("haspath");
                if (Vector3.Distance(transform.position , currentDestination) <= 0.1f)
				{
					currentDestination = path.PopPoint(); /* Get next destination */
					currentDestination.y = transform.position.y; // Lock y
					
					if(Mathf.Abs(currentDestination.x - transform.position.x) > 5)
					{
						orientation = Orientation.Horizontal;

						if (currentDestination.x > transform.position.x)
						{
							transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z), 0.8f).OnComplete(() => cam.ReCalculateRotation()); ;
							left = false;
						}
						else
						{
							transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y  + 90, transform.eulerAngles.z), 0.8f).OnComplete( () => cam.ReCalculateRotation());
							left = true;

						}
						first = false;
					}
					else
					{
						orientation = Orientation.Vertical;
						if (!first)
						{
							float newYaw = transform.eulerAngles.y;
							if (left) { newYaw -= 90; }
							else { newYaw += 90; }
							transform.DORotate(new Vector3(transform.eulerAngles.x, newYaw, transform.eulerAngles.z), 0.8f).OnComplete(() => cam.ReCalculateRotation()); ;
						}
					}
					PlayerManager.instance.playerOrientation = orientation;
					
				}
				Vector3 direction = (currentDestination - transform.position).normalized;
				rigid.velocity = direction * Speed * Time.deltaTime;
			}
		}
        
    }
}
