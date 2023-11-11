using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public GameObject[] enemyTilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 20;
    public Transform playerTransform;
    public int numberOfTiles = 3;
    private List<GameObject> activeTiles = new List<GameObject>();
    private int count = 5;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyTile(0);
        spawnTile(0);
        spawnTile(0);
        spawnTile(0);
        spawnTile(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > zSpawn - (numberOfTiles * tileLength)) 
        {
            if (count == 5)
            {
                count = 0;
                spawnEnemyTile(0);
            }
            spawnTile(0);
            count++;
            DeleteTile();
        }
    }

    private void spawnTile(int tileIndex)
    {
        
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void spawnEnemyTile(int tileIndex)
    {
        GameObject go = Instantiate(enemyTilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
}
