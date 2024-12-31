using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class PathRetriever : MonoBehaviour
	{
    	
		[SerializeField] private WalkerCreator walkerCreator;

        [SerializeField] private Queue<Vector3> breadcrumbs;

		public GameObject turnSign;

		private void Awake()
		{
			breadcrumbs = new Queue<Vector3>();
	        walkerCreator.OnTurnPlaced += AddPathPoint;
        }


		private void AddPathPoint(Vector3 point)
        {
	        var sign = Instantiate(turnSign);
	        sign.transform.position = new Vector3(point.x + .5f, 0, point.y + .5f);
	        breadcrumbs.Enqueue(sign.transform.position);
        }
        
		public Vector3 PopPoint()
		{
			if (breadcrumbs.TryDequeue(out Vector3 point))
			{
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
