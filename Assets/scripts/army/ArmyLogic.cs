using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class ArmyLogic : MonoBehaviour
{
    private int zombCount = 0;
    private int currentZombs = 0;
    public GameObject player;
    private List<GameObject> zombies = new List<GameObject>();
    private List<Tuple<float, float>> zombieSpawnPositions = new List<Tuple<float, float>>();
    private float moveSpeed = 3;
    private float strafeSpeed = 4;
    private int updatedScore;
    private int recentScore = 0;
    private GameObject mostRecentZombie;
    public GameObject[] prefabsToInstantiate;
    private int prefabIndex;
    private List<float> xSpawnPoints = new List<float> { 0f, -.5f, .5f, -1f, 1f, -1.5f, 1.5f, -2f, 2f, -2.5f, 2.5f, -3f, 3f, -3.5f, 3.5f, -4f, 4f, -4.5f, 4.5f, -5f, 5f };
    private List<float> zSpawnPoints = new List<float> { -1f, -2f, -3f, -4f };
    private int row;
    // Start is called before the first frame update
    void Start()
    {
        prefabIndex = UnityEngine.Random.Range(0, 39);
    }

    // Update is called once per frame
    void Update()
    {
        updatedScore = collectableControl.scoreCount;
        if (updatedScore != recentScore)
        {
            updateZombs();
        }
        foreach (GameObject zombie in zombies)
        {
            zombie.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World); //moves the character forward

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  //if player is not heading outside the boundary
            {
                if (zombie.gameObject.transform.position.x > levelBoundary.leftSide + zombieSpawnPositions[zombies.IndexOf(zombie)].Item1) //if player is pressing left key, move left
                {
                    zombie.transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed);
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //if player is not heading outside the boundary
            {
                if (zombie.transform.position.x < (levelBoundary.rightSide + zombieSpawnPositions[zombies.IndexOf(zombie)].Item1)) //if player is pressing right key, move right
                {
                    zombie.transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed * -1);
                }
            }
        }
    }

    public void updateZombs()
    {

        zombCount = collectableControl.scoreCount / 10;

        if (zombCount > 84)
        {
            zombCount = 84;
        }

        while (currentZombs != zombCount)
        {
            if (currentZombs < zombCount)
            {
                currentZombs++;
                prefabIndex = UnityEngine.Random.Range(0, 39);
                if (currentZombs <= 21)
                {
                    row = 0;
                }
                else if (currentZombs > 21 && currentZombs <= 42)
                {
                    row = 1;
                }
                else if (currentZombs > 42 && currentZombs <= 62)
                {
                    row = 2;
                }
                else
                {
                    row = 3;
                }
                float newPosX = xSpawnPoints[currentZombs - (21 * row) - 1];
                float newPosZ = player.transform.position.z + zSpawnPoints[row];
                Vector3 spawnPosition = new Vector3(player.transform.position.x + xSpawnPoints[currentZombs - (21 * row) - 1], .5f, player.transform.position.z + zSpawnPoints[row]); // You can change these coordinates
                Quaternion spawnRotation = Quaternion.identity; // No rotation

                // Instantiate the prefab
                GameObject instantiatedPrefab = Instantiate(prefabsToInstantiate[prefabIndex], spawnPosition, spawnRotation);


                // Add the instantiated zombie to the list
                zombies.Add(instantiatedPrefab);
                zombieSpawnPositions.Add(new Tuple<float, float>(newPosX, newPosZ));
                
            }
            else if (currentZombs > zombCount)
            {
                // Remove the most recently instantiated zombie
                if (zombies.Count > 0 && zombies.Last() != null)
                {
                    Destroy(zombies.Last());
                    zombies.Remove(zombies.Last());
                    zombieSpawnPositions.Remove(zombieSpawnPositions.Last());
                }
                currentZombs--;
            }
        }

        

    }
}
