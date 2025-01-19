using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WalterWalk
{
    public class PathRetriever : MonoBehaviour
	{
    	
		[SerializeField] private WalkerCreator walkerCreator;

        [SerializeField] private Queue<Vector3> breadcrumbs;

		public GameObject turnSign;

		Vector3 lastPoint;

		private int numPaths = 0;

		private void Awake()
		{
			breadcrumbs = new Queue<Vector3>();
	        walkerCreator.OnTurnPlaced += AddPathPoint;
        }

        private void Start()
        {
			lastPoint = PlayerManager.instance.player.transform.position;
        }

        private void AddPathPoint(Vector3 point)
        {
	        //var sign = Instantiate(turnSign);
	        //sign.transform.position = new Vector3(point.x + .5f, 0, point.y + .5f);
	        breadcrumbs.Enqueue(new Vector3(point.x + .5f, 0, point.y + .5f));


   //         Transform child = sign.transform.Find("GameObject");
			//// sign offset


			//if (Mathf.Abs(lastPoint.x - point.x) > 10)
			//{
			//	// left turn
			//	if (lastPoint.x > point.x)
			//	{
			//		if (child != null)
			//		{
			//			child.eulerAngles = new Vector3(0, 90, 0);
			//		}
			//	}
			//}
	

        }
        
		public Vector3 PopPoint()
		{
			if (breadcrumbs.TryDequeue(out Vector3 point))
			{
				if (numPaths != 0)
				{
					GameManager.Instance?.mapCreator?.DetroyOldRoad();

				}
				numPaths++;
				return point;
			}
			else
			{
				Vector3 lastPoint = new Vector3(walkerCreator.GetWalkerPosition().x + .5f, 0, walkerCreator.GetWalkerPosition().y + .5f);
				return lastPoint;
			}
		}
    }
}
