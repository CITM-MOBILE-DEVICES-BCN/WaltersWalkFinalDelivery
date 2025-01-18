using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;


[System.Serializable]
public class BuildingInfo
{
	public Vector3 size;
	public GameObject building;
}

namespace WalterWalk
{
	public class BuildingFactory : MonoBehaviour
	{
		public float chanceToBuild = 1f;
		public float [] offsetFromCenter;
		
		private Tilemap tileMap;

		public List<BuildingInfo> BuildingCollection = new List<BuildingInfo>();
		
		private WalkerCreator walkerCreator;

		public List<DestroyableMapTile> tilesToDestroy;

		private void Awake()
		{
			tileMap = GetComponentInChildren<Tilemap>();
	        walkerCreator = GetComponent<WalkerCreator>();
	        walkerCreator.OnChanceSpawnBuilding += ChanceToBuild;

			for (int i = 0; i < BuildingCollection.Count; ++i)
			{
				BuildingCollection[i].size = Vector3.Scale( BuildingCollection[i].building.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size, BuildingCollection[i].building.transform.localScale);

            }

        }
		private async void ChanceToBuild(Vector3 position, Vector2 direction, Vector3Int tilePosition)
		{
			float yRotation = 0f;

			int i = UnityEngine.Random.Range(0, 2);
			if (direction == Vector2.down)
			{
				switch (i)
				{ /* Left or right side of road */
					case 0: position.x += offsetFromCenter[UnityEngine.Random.Range(0, offsetFromCenter.Length-1)];  break;
					case 1: position.x += offsetFromCenter[UnityEngine.Random.Range(0, offsetFromCenter.Length-1)] * -1; yRotation = 180f; break;
				}
			}
			else
			{
                switch (i)
                { /* Left or right side of road */
                    case 0: position.z += offsetFromCenter[UnityEngine.Random.Range(0, offsetFromCenter.Length-1)]; yRotation = -90; break;
                    case 1: position.z += offsetFromCenter[UnityEngine.Random.Range(0, offsetFromCenter.Length-1)] * -1; yRotation = 90; break;
                }
            }

			TileBase tile = tileMap.GetTile(tilePosition);
			
			if(UnityEngine.Random.value <= chanceToBuild)
			{
				await Task.Delay(100); /* wait a bit in case some road spawns up ahead */
				SpawnBuilding(position, tile, yRotation);
			}
			else{
				print("no building");
			}
		}

		private void SpawnBuilding(Vector3 position, TileBase parent, float yRotation = 0)
		{
			BuildingPlacer placer = new BuildingPlacer(position);
			Vector3 size = placer.CreateSize();
			List<GameObject> fittableBuildings = new List<GameObject>();


			for (int i = 0; i < BuildingCollection.Count; ++i)
			{
				if (DoesBuildingFit(BuildingCollection[i].size, size, yRotation))
				{
					fittableBuildings.Add(BuildingCollection[i].building);

				}
			}
			
			if(fittableBuildings.Count > 0)
			{
				if(fittableBuildings.Count > 1)
				{
					print("multiple possible buildings");
				}

				var building = Instantiate(fittableBuildings[ UnityEngine.Random.Range(0,fittableBuildings.Count )]);
				building.transform.position = position;
				building.transform.eulerAngles = new  Vector3(0, yRotation, 0);
				GameManager.Instance.mapCreator.AddDestroyableBuilding(building);
                //WalkerCreator.Instance.AddDestroyableBuilding(building);
			}
			else
			{
				print("no buildings ot spawn");
			}
			
		}

		private bool DoesBuildingFit(Vector3 building_size, Vector3 parcel_size, float YRotation)
		{
			//if(YRotation == 90f ||  YRotation == -90f) /* if the building is rotated change the axis */
			//{
			//	float buff = building_size.z;
			//	building_size.z = building_size.x;
			//	building_size.x = buff;
			//}

			return (building_size.x <= parcel_size.x && building_size.y <= parcel_size.y && building_size.z <= parcel_size.z);
		}
	
    }
}
