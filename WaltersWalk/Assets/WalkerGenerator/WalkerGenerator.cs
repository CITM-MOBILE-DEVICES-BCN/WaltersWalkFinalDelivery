using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerCreator : MonoBehaviour
{
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }

    //Variables
    public Grid[,] gridHandler;
    public List<WalkerObject> Walkers;
    public Tilemap tileMap;
    public RuleTile Floor;
    public RuleTile Road;
    public RuleTile RoadVertical;    
    public RuleTile Wall;
    public int MapWidth = 30;
    public int MapHeight = 30;

    [Range(0f, 1f)] public float chance_to_create;

    public int MaximumWalkers = 10;
    public int TileCount = default;
    public float FillPercentage = 0.4f;
    public float WaitTime = 0.05f;

    [Header("Constraints")]
    public bool multiplePaths = false;
    public int MINIMUM_TILES_FOR_TURN = 10;
    public float roadChance = 0.05f;

    private int tilesSinceRoad = 0;
    private int tilesSinceTurn = 0;

    void OnEnable()
    {


        InitializeGrid();
    }

    void InitializeGrid()
    {
        gridHandler = new Grid[MapWidth, MapHeight];

        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                gridHandler[x, y] = Grid.EMPTY;
            }
        }

        Walkers = new List<WalkerObject>();
        // MapHeight = start at top center
        Vector3Int TileCenter = new Vector3Int(gridHandler.GetLength(0) / 2, /*gridHandler.GetLength(1) / 2*/ MapHeight - 1, 0);

        WalkerObject curWalker = new WalkerObject(new Vector2(TileCenter.x, TileCenter.y), Vector2.down, 0.5f);
        gridHandler[TileCenter.x, TileCenter.y] = Grid.FLOOR;
        tileMap.SetTile(TileCenter, Floor);
        Walkers.Add(curWalker);

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
            int id = Random.Range(0, 2);


                switch (id)
                {
                    case 0: return Vector2.left;
                    case 1: return Vector2.right;
                }
        }

        return Vector2.down;
        //int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);

        //switch (choice)
        //{
        //    case 0:
        //        return Vector2.down;
        //    case 1:
        //        return Vector2.left;
        //    case 2:
        //        return Vector2.down;
        //    case 3:
        //        return Vector2.right;
        //    default:
        //        return Vector2.zero;
        //}
    }

    IEnumerator CreateFloors()
    {
        while ((float)TileCount / (float)gridHandler.Length < FillPercentage)
        {
            bool end = false;
            bool hasCreatedFloor = false;
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector3Int curPos = new Vector3Int((int)curWalker._position.x, (int)curWalker._position.y, 0);

                if (gridHandler[curPos.x, curPos.y] != Grid.FLOOR)
                {
                    RuleTile tile = Floor;
                    if (tilesSinceRoad > 6 && Random.value <= tilesSinceRoad/100f)
                    {
                        tilesSinceRoad = 0;

                        if (curWalker._direction != Vector2.down)
                        {
                            tile = RoadVertical;
                        }
                        else { tile = Road; }
                       
                    }
                    else
                    {
                        tilesSinceRoad++;
                    }
                    tileMap.SetTile(curPos, tile);

                    tilesSinceTurn++; // keep track of how many tiles spawned since last turn
                    TileCount++;
                    gridHandler[curPos.x, curPos.y] = Grid.FLOOR;
                    hasCreatedFloor = true;
                }

                if (curWalker._position.y == 1) /* At the bottom end of the map */
                {
                    end = true; break;
                }

                // if the walker is at the left or right bounds force a turn
                if (curWalker._position.x >=  MapWidth - 2 || curWalker._position.x <= 1)
                {
                    Redirect(curWalker);
                    break;
                }


            }

            if (end) { break; }

            //Walker Methods
            if (tilesSinceTurn >= MINIMUM_TILES_FOR_TURN)
            {
                if (ChanceToRedirect()) { tilesSinceTurn = 0; }
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
            if (UnityEngine.Random.value < tilesSinceTurn/100f )
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
            FoundWalker._position.x = Mathf.Clamp(FoundWalker._position.x, 1, gridHandler.GetLength(0) - 2);
            FoundWalker._position.y = Mathf.Clamp(FoundWalker._position.y, 1, gridHandler.GetLength(1) - 2);
            Walkers[i] = FoundWalker;
        }
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
