using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Threading.Tasks;
using WalterWalk;

public class WalkerCreator : MonoBehaviour
{
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }

    public int maxTilesInstanciated;
    public int TileCount = default;

    public Tilemap tileMap;
    public RuleTile Floor;
    public RuleTile FloorVertical;
    public RuleTile Road;
    public RuleTile RoadVertical;    
    public RuleTile Wall;
    private float MapWidth = 30;
    public float MapHeight = 30;

    [Tooltip("Percentage chance of duplicating walker and creating another path")]
    [Range(0f, 1f)] public float chance_to_create;

    public float WaitTime = 0.05f;

    [Header("Constraints")]
    public int MaximumWalkers = 1;
    public bool multiplePaths = false;
    public int MINIMUM_TILES_FOR_TURN = 10;
    public int MINIMUM_TILES_FOR_ROAD = 4;

    [Tooltip("Increase for less roads, decrease for more")]
    public float roadSparcity = 200f;
    [Tooltip("Increase for less turns, decrease for more")]
    public float turnSparcity = 200f;

    private int tilesSinceRoad = 0;
    private int tilesSinceTurn = 0;

    private Grid[,] gridHandler;
    private List<WalkerObject> Walkers;
    
	public event Action<Vector3> OnTurnPlaced; //  Notify the PathRetriever of the new turn
	public event Action<Vector3, Vector2, Vector3Int> OnChanceSpawnBuilding; // Notify the BuildingFactory of the chance to spawn

	private Queue<List<DestroyableMapTile>> tilesToDestroy;
	private List<DestroyableMapTile> currentRoad;
	
	private Queue<List<GameObject>> buildingsToDestroy;
	private List<GameObject> currentRoadBuildings;
	
	public WalkerObject walkerPrefab;

    void OnEnable()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {

        Walkers = new List<WalkerObject>();
        // MapHeight = start at top center
        Vector3Int TileCenter = new Vector3Int( ((int)MapWidth) / 2, /*gridHandler.GetLength(1) / 2*/ ((int) MapHeight) - 1, 0);

	    WalkerObject curWalker = Instantiate(walkerPrefab);
	    curWalker.Init(new Vector2(TileCenter.x, TileCenter.y), Vector2.down, 0.5f);
	    
	    //WalkerObject curWalker = new WalkerObject(new Vector2(TileCenter.x, TileCenter.y), Vector2.down, 0.5f);
       // gridHandler[TileCenter.x, TileCenter.y] = Grid.FLOOR;
        tileMap.SetTile(TileCenter, Floor);
        Walkers.Add(curWalker);
        
        
        MapWidth = Mathf.Infinity  ;

        TileCount++;

        StartCoroutine(CreateFloors());
    }

    Vector2 GetDirection(Vector2 currentDir)
    {
        if (currentDir != Vector2.down)
        {
            return Vector2.down;
        }
        else
        {
            int id = UnityEngine.Random.Range(0, 2);


                switch (id)
                {
                    case 0: return Vector2.left;
                    case 1: return Vector2.right;
                }
        }

        return Vector2.down;

    }

    IEnumerator CreateFloors()
    {
        while (true)
        {
            if (TileCount <= maxTilesInstanciated)
            {

                bool end = false;
                bool hasCreatedFloor = false;
                foreach (WalkerObject curWalker in Walkers)
                {
                    Vector3Int curPos = new Vector3Int((int)curWalker._position.x, (int)curWalker._position.y, 0);


                    RuleTile tile = FloorVertical;
	                if ((tilesSinceRoad > MINIMUM_TILES_FOR_ROAD && UnityEngine.Random.value <= tilesSinceRoad / roadSparcity) || tilesSinceRoad > 148)
                    {
                        tilesSinceRoad = 0;
                        tilesSinceTurn = 0;

                        if (curWalker._direction != Vector2.down)
                        {
                            tile = RoadVertical;
                        }
                        else { tile = Road; }

                    }
                    else
                    {

                        // Chance to spawn a building
                        OnChanceSpawnBuilding?.Invoke(curWalker.GlobalPosition(), curWalker._direction, curPos);

                        tilesSinceRoad++;
                        if (curWalker._direction != Vector2.down)
                        {
                            tile = Floor;
                        }

                    }
                    tileMap.SetTile(curPos, tile);

                    tilesSinceTurn++; // keep track of how many tiles spawned since last turn
                    TileCount++;
                    hasCreatedFloor = true;

                    //if (curWalker._position.y == 1) /* At the bottom end of the map */
                    //{
                    //    end = true; break;
                    //}

                }

                if (end) { break; }

                //Walker Methods
                if (tilesSinceTurn >= MINIMUM_TILES_FOR_TURN)
                {
                    if (ChanceToRedirect())
                    {
                        AddRoadToQueue();
                        tilesSinceTurn = 0; tilesSinceRoad = 0;
                        if (OnTurnPlaced != null) OnTurnPlaced.Invoke(Walkers[0]._position);
                    }
                }
                UpdatePosition();

                if (hasCreatedFloor)
                {
	                yield return null;
                    //yield return new WaitForSeconds(WaitTime);
                }
                if (multiplePaths)
                {
                    ChanceToCreate();
                }

            }
            yield return null;
        }
        yield return null;
    }

    void ChanceToRemove()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i]._ChanceToChange && Walkers.Count > 1)
            {
                Walkers.RemoveAt(i);
                break;
            }
        }
    }

    bool ChanceToRedirect()
    {
        bool redirect = false;

        for (int i = 0; i < Walkers.Count; i++)
        {
	        if (UnityEngine.Random.value < tilesSinceTurn/ turnSparcity )
            {
                Redirect(Walkers[i]);
                redirect = true;
            }
        }

        return redirect;
    }

    void Redirect(WalkerObject walker)
    {
        var dir = walker._direction;
        while (walker._direction == dir)
        {
            walker._direction = GetDirection(walker._direction);
        }
    }

    void ChanceToCreate()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i]._ChanceToChange && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDirection = GetDirection(Walkers[i]._direction);
                Vector2 newPosition = Walkers[i]._position;

                WalkerObject newWalker = new WalkerObject(newPosition, newDirection, chance_to_create);
                Walkers.Add(newWalker);
            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            WalkerObject FoundWalker = Walkers[i];
            FoundWalker._position += FoundWalker._direction;
            //FoundWalker._position.x = Mathf.Clamp(FoundWalker._position.x, 1, gridHandler.GetLength(0) - 2);
            //FoundWalker._position.y = Mathf.Clamp(FoundWalker._position.y, 1, gridHandler.GetLength(1) - 2);
            Walkers[i] = FoundWalker;
        }
    }
    
	public Vector2 GetWalkerPosition()
	{
		return Walkers[0]._position;
	}

    void DeleteAllChildObjects()
    {
        // Get all child GameObjects of the GameObject
        GameObject[] childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }

        // Iterate through each child GameObject and destroy it
        foreach (GameObject child in childObjects)
        {
            Destroy(child);
        }
    }


	private void Start()
	{
		GameManager.Instance.mapCreator = this;
	}
	
	public void AddDestroyableTile(DestroyableMapTile tile)
	{
		if (currentRoad == null){currentRoad = new List<DestroyableMapTile>();}
		currentRoad.Add(tile);
	}
	
	public void AddDestroyableBuilding(GameObject building)
	{
		if (currentRoadBuildings == null){currentRoadBuildings = new List<GameObject>();}
		currentRoadBuildings.Add(building);
	}
	
	private void AddRoadToQueue()
	{
		if(tilesToDestroy == null){tilesToDestroy = new Queue<List<DestroyableMapTile>>();}
		tilesToDestroy.Enqueue(new List<DestroyableMapTile> ( currentRoad ) ); // enqueue a new list so that it wont be lost when cleared
        
		if (currentRoad == null) { currentRoad = new List<DestroyableMapTile>(); }
		currentRoad.Clear();
        
        
        
		if(buildingsToDestroy == null){buildingsToDestroy = new Queue<List<GameObject>>();}
		if(currentRoadBuildings != null) buildingsToDestroy.Enqueue(new List<GameObject>(currentRoadBuildings) ) ;
		
		if (currentRoadBuildings == null){currentRoadBuildings = new List<GameObject>();}
		currentRoadBuildings.Clear();
        
	}
	
	public async Task DetroyOldRoad() /* called from the Path follower after crossing to the next road */
	{
		
		await Task.Delay(8000);
		
		List<DestroyableMapTile> old =  tilesToDestroy.Dequeue();
		for(int i = 0; i < old.Count; ++i)
		{
            TileCount--;
			Destroy(old[i].gameObject);
		}
		
		List<GameObject> oldBuildings = buildingsToDestroy.Dequeue();
		for(int i = 0; i < oldBuildings.Count; ++i)
		{
			Destroy(oldBuildings[i].gameObject);
		}
		//TODO clean up actual tilemap tiles
		//tileMap.ClearAllTiles();
	}
	
	void OnApplicationQuit()
	{
		Debug.Log("Exiting Play Mode: Performing cleanup.");
		
		buildingsToDestroy.Clear();
		currentRoad.Clear();
		tilesToDestroy.Clear();
		currentRoadBuildings.Clear();

	}
}
