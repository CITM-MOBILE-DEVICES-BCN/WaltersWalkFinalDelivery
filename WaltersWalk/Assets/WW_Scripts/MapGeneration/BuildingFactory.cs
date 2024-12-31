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
		public float offsetFromCenter = 18f;
		
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

			int i = UnityEngine.Random.Range(0, 2);
			if (direction == Vector2.down)
			{
				switch (i)
				{ /* Left or right side of road */
					case 0: position.x += offsetFromCenter; break;
					case 1: position.x += offsetFromCenter * -1; break;
				}
			}
			else
			{
                switch (i)
                { /* Left or right side of road */
                    case 0: position.z += offsetFromCenter; break;
                    case 1: position.z += offsetFromCenter * -1; break;
                }
            }

			TileBase tile = tileMap.GetTile(tilePosition);
			
			if(UnityEngine.Random.value <= chanceToBuild)
			{
				await Task.Delay(100); /* wait a bit in case some road spawns up ahead */
				SpawnBuilding(position, tile);
			}
			else{
				print("no building");
			}
		}

		private void SpawnBuilding(Vector3 position, TileBase parent)
		{
			BuildingPlacer placer = new BuildingPlacer(position);
			Vector3 size = placer.CreateSize();
			List<GameObject> fittableBuildings = new List<GameObject>();


			for (int i = 0; i < BuildingCollection.Count; ++i)
			{
				if (DoesBuildingFit(BuildingCollection[i].size, size))
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
				GameManager.Instance.mapCreator.AddDestroyableBuilding(building);
                //WalkerCreator.Instance.AddDestroyableBuilding(building);
			}
			else
			{
				print("no buildings ot spawn");
			}
			
		}

		private bool DoesBuildingFit(Vector3 building_size, Vector3 parcel_size)
		{
			return (building_size.x <= parcel_size.x && building_size.y <= parcel_size.y && building_size.z <= parcel_size.z);
		}
	
    }
}
