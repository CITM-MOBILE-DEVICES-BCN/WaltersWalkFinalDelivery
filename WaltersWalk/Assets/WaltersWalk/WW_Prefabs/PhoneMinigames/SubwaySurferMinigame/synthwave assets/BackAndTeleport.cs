using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollingFloor : MonoBehaviour
{
    public GameObject tilePrefab; // The floor tile prefab to be cloned
    public Transform despawnCheck; // Empty GameObject to check when tiles should despawn
    public float tileLength = 10f; // Length of each tile
    public int initialTiles = 10; // Number of tiles to start with
    public float scrollSpeed = 5f; // Speed at which the tiles scroll
    public AudioSource audioSource; // AudioSource component for background music

    private Queue<GameObject> tilesQueue = new Queue<GameObject>();

    void Start()
    {
        // Play the audio track starting at 0:58
        if (audioSource != null)
        {
            audioSource.time = 58f;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource assigned to InfiniteScrollingFloor.");
        }

        // Initialize the floor with the given number of tiles
        for (int i = 0; i < initialTiles; i++)
        {
            Vector3 position = transform.position + new Vector3(0, 0, i * tileLength - 65);
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
            tilesQueue.Enqueue(tile);
        }
    }

    void Update()
    {
        foreach (GameObject tile in tilesQueue)
        {
            // Move each tile backwards
            tile.transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);
        }

        // Check if the first tile is past the despawnCheck
        GameObject firstTile = tilesQueue.Peek();
        if (firstTile.transform.position.z + tileLength / 2 < despawnCheck.position.z)
        {
            // Remove the tile from the queue and destroy it
            tilesQueue.Dequeue();
            Destroy(firstTile);

            // Add a new tile at the end of the line
            Vector3 position = tilesQueue.Peek().transform.position + new Vector3(0, 0, tileLength);
            GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
            tilesQueue.Enqueue(newTile);
        }
    }
}
