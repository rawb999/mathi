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
    public int numberOfTiles = 4;
    private List<GameObject> activeTiles = new List<GameObject>();
    private int count = 0;
    public int numberOfEnemies;
    public GameObject enemyPrefab;
    private List<Vector3> enemySpawnPoints = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemies = 1;
        spawnTile();
        spawnEnemyTile(0);
        SpawnEnemies(numberOfEnemies);
        spawnTile();
        spawnEnemyTile(0);
        SpawnEnemies(numberOfEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > zSpawn - (numberOfTiles * tileLength)) 
        {
            if (count == 1)
            {
                count = 0;
                spawnEnemyTile(0);
                SpawnEnemies(numberOfEnemies);
            }
            else
            {
                count++;
                spawnTile();
            }
            
            DeleteTile();
        }
        ActivateEnemiesNearPlayer();
    }

    private void spawnTile()
    {
        
        GameObject go = Instantiate(tilePrefabs[Random.Range(0, 5)], transform.forward * zSpawn, transform.rotation);
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
        GameObject go = Instantiate(enemyTilePrefabs[Random.Range(0, 5)], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    
    void SpawnEnemies(int enemiesQuant)
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-7f, 7f), .5f, (zSpawn - 20) + Random.Range(0, 7f));
            enemySpawnPoints.Add(randomPosition);
        }

        numberOfEnemies += 1;

        if (collectableControl.waveNumber > 9)
        {
            numberOfEnemies += 1;
        }

        if (collectableControl.waveNumber > 9)
        {
            numberOfEnemies += 2;
        }
        if (collectableControl.waveNumber > 14)
        {
            numberOfEnemies += 3;
        }
        if (collectableControl.waveNumber > 19)
        {
            numberOfEnemies += 4;
        }
    }

    void ActivateEnemiesNearPlayer()
    {
        for (int i = enemySpawnPoints.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(playerTransform.position, enemySpawnPoints[i]) < 20f)
            {
                Quaternion randomRotation = Quaternion.Euler(0, 180, 0);
                Instantiate(enemyPrefab, enemySpawnPoints[i], randomRotation); // Instantiate the enemy
                enemySpawnPoints.RemoveAt(i); // Remove the spawn point from the list
            }
        }
    }

}
