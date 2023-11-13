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
    public int numberOfEnemies;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnTile();
        spawnTile();
        spawnTile();
        spawnTile();
        spawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > zSpawn - (numberOfTiles * tileLength)) 
        {
            if (count == 5)
            {
                numberOfEnemies = Random.Range(1, 5);
                count = 0;
                spawnEnemyTile(0);
                SpawnEnemies(numberOfEnemies);
            }
            spawnTile();
            count++;
            DeleteTile();
        }
    }

    private void spawnTile()
    {
        
        GameObject go = Instantiate(tilePrefabs[Random.Range(0, 6)], transform.forward * zSpawn, transform.rotation);
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
    
    void SpawnEnemies(int enemiesQuant)
    {
        for (int i = 0; i < enemiesQuant; i++)
        {
            // instantiate the prefab at a random position
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), .5f, zSpawn - 20);
            Quaternion randomRotation = Quaternion.Euler(0, 180, 0);
            

            Instantiate(enemyPrefab, randomPosition, randomRotation);
        }
    }
    
}
