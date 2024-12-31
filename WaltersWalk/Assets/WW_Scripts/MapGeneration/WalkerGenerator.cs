using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WalterWalk;

public class WalkerCreator : MonoBehaviour
{
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }

    //Variables
    
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
	private int TileCount = default;
    
	public event Action<Vector3> OnTurnPlaced; //  Notify the PathRetriever of the new turn
	public event Action<Vector3, Vector2> OnChanceSpawnBuilding; // Notify the BuildingFactory of the chance to spawn

    void OnEnable()
    {


        InitializeGrid();
    }

    void InitializeGrid()
    {

        Walkers = new List<WalkerObject>();
        // MapHeight = start at top center
        Vector3Int TileCenter = new Vector3Int( ((int)MapWidth) / 2, /*gridHandler.GetLength(1) / 2*/ ((int) MapHeight) - 1, 0);

        WalkerObject curWalker = new WalkerObject(new Vector2(TileCenter.x, TileCenter.y), Vector2.down, 0.5f);
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
            bool end = false;
            bool hasCreatedFloor = false;
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector3Int curPos = new Vector3Int((int)curWalker._position.x, (int)curWalker._position.y, 0);

                
                    RuleTile tile = FloorVertical;
                    if (tilesSinceRoad > MINIMUM_TILES_FOR_ROAD && UnityEngine.Random.value <= tilesSinceRoad/ roadSparcity)
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
	                    OnChanceSpawnBuilding?.Invoke(curWalker.GlobalPosition(), curWalker._direction);
                        //BuildingPlacer placer = new BuildingPlacer(curWalker.GlobalPosition());
                    	//Vector3 size = placer.CreateSize();
                    	
                        tilesSinceRoad++;
                        if (curWalker._direction != Vector2.down)
                        {
                            tile = Floor;
                        }

                    }
                    tileMap.SetTile(curPos, tile);

                    tilesSinceTurn++; // keep track of how many tiles spawned since last turn
                    TileCount++;
                    //gridHandler[curPos.x, curPos.y] = Grid.FLOOR;
                    hasCreatedFloor = true;
                

                if (curWalker._position.y == 1) /* At the bottom end of the map */
                {
                    end = true; break;
                }

            }

            if (end) { break; }

            //Walker Methods
            if (tilesSinceTurn >= MINIMUM_TILES_FOR_TURN)
            {
	            if (ChanceToRedirect()) 
	            { 
		            tilesSinceTurn = 0; tilesSinceRoad = 0; 
		            if(OnTurnPlaced != null) OnTurnPlaced.Invoke(Walkers[0]._position);
	            }
            }
            UpdatePosition();

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(WaitTime);
            }
            if (multiplePaths)
            {
                ChanceToCreate();
            }

        }

        //StartCoroutine(CreateWalls());

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

    IEnumerator CreateWalls()
    {
        for (int x = 0; x < gridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; y++)
            {
                if (gridHandler[x, y] == Grid.FLOOR)
                {
                    bool hasCreatedWall = false;

                    if (gridHandler[x + 1, y] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x + 1, y, 0), Wall);
                        gridHandler[x + 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x - 1, y] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x - 1, y, 0), Wall);
                        gridHandler[x - 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y + 1] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x, y + 1, 0), Wall);
                        gridHandler[x, y + 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y - 1] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x, y - 1, 0), Wall);
                        gridHandler[x, y - 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }

                    if (hasCreatedWall)
                    {
                        yield return new WaitForSeconds(WaitTime);
                    }
                }
            }
        }
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

}
