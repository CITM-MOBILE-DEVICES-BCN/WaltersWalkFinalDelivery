using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public GameObject roadTilePrefab; // The road tile prefab
    public GameObject spawnReference; // Reference GameObject for spawn position
    public int initialTileCount = 10; // Number of tiles to initialize
    public float tileLength = 10f; // Length of each tile
    public float speed = 5f; // Speed of the moving floor
    public Vector3 positionOffset = Vector3.zero; // Offset for tile positioning

    private Queue<GameObject> roadTiles = new Queue<GameObject>();
    private float spawnZPosition = 0f; // Z position to spawn the next tile
    private float despawnDistance = -15f; // Distance behind to despawn tiles

    void Start()
    {
        // Initialize road tiles
        for (int i = 0; i < initialTileCount; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // Move the floor backward
        foreach (var tile in roadTiles)
        {
            tile.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        // Check if the first tile needs to be despawned
        if (roadTiles.Peek().transform.position.z + tileLength < despawnDistance)
        {
            RecycleTile();
        }
    }

    private void SpawnTile()
    {
        Vector3 referencePosition = spawnReference != null ? spawnReference.transform.position : transform.position; // Use spawnReference position if available
        GameObject tile = Instantiate(roadTilePrefab, new Vector3(0, 0, spawnZPosition) + referencePosition + positionOffset, Quaternion.identity, transform);
        roadTiles.Enqueue(tile);
        spawnZPosition += tileLength;
    }

    private void RecycleTile()
    {
        Vector3 referencePosition = spawnReference != null ? spawnReference.transform.position : transform.position; // Use spawnReference position if available
        GameObject tile = roadTiles.Dequeue(); // Remove the oldest tile
        tile.transform.position = new Vector3(0, 0, spawnZPosition) + referencePosition + positionOffset; // Move it to the next position
        spawnZPosition += tileLength;
        roadTiles.Enqueue(tile); // Add it back to the queue
    }
}
