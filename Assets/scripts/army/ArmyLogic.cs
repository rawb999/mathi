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
    private int recentScore = 1;
    private GameObject mostRecentZombie;
    public GameObject[] prefabsToInstantiate;
    private int prefabIndex;
    private List<float> xSpawnPoints = new List<float> { 0f, -.5f, .5f, -1f, 1f, -1.5f, 1.5f, -2f, 2f, -2.5f, 2.5f, -3f, 3f, -3.5f, 3.5f, -4f, 4f, -4.5f, 4.5f, -5f, 5f };
    private List<float> zSpawnPoints = new List<float> { -1f, -2f, -3f, -4f };
    private int row;
    public Animator playerAnimator;
    private List<string> animations = new List<string> { "attack", "attack2", "spellcast"};
    public static bool inFight = false;
    public float stoppingDistance = 1.0f; // This is the distance at which the zombie will stop from the enemy
    private Dictionary<GameObject, Vector3> originalOffsets;
    bool startedMovingBack = false;


    // Start is called before the first frame update
    void Start()
    {
        originalOffsets = new Dictionary<GameObject, Vector3>();
        prefabIndex = UnityEngine.Random.Range(0, 39);
    }

    // Update is called once per frame
    void Update()
    {
        updatedScore = collectableControl.scoreCount;
        if (updatedScore != recentScore)
        {
            print("updated");
            recentScore = updatedScore;
            playerAnimator.SetTrigger(animations[UnityEngine.Random.Range(0, 3)]);
            updateZombs();
        }
        if (!inFight)
        {
            startedMovingBack = false;
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
        else
        {
            
            Enemy[] allEnemies = FindObjectsOfType<Enemy>();
            foreach (GameObject zombie in zombies)
            {
                Vector3 originalPosition = player.transform.position + originalOffsets[zombie];
                float distanceToOriginal = Vector3.Distance(zombie.transform.position, originalPosition);
                float minDistance = Mathf.Infinity;
                Vector3 currentPosition = zombie.transform.position; // Use the position of the current zombie
                Enemy nearestEnemy = null;

                foreach (Enemy enemyScript in allEnemies)
                {
                    float distance = Vector3.Distance(currentPosition, enemyScript.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemy = enemyScript;
                    }
                }

                if (nearestEnemy != null)
                {
                    if (minDistance > stoppingDistance && startedMovingBack == false)
                    {
                        // Move towards the nearest enemy if it's beyond the stopping distance
                        MoveTowards(zombie, nearestEnemy.transform.position);
                    }
                    else 
                    {
                        // Move back to the original position if within the stopping distance
                        Enemy.dead = true;
                        startedMovingBack = true;
                        MoveTowards(zombie, originalPosition);
                        float closeEnoughDistance = 0.1f;
                        if (Vector3.Distance(zombie.transform.position, originalPosition) < closeEnoughDistance)
                        {
                            print("called");
                            fight.endFight();
                            Enemy.dead = false;
                        }
                    }
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
                Vector3 offset = instantiatedPrefab.transform.position - player.transform.position;
                originalOffsets[instantiatedPrefab] = offset;
                zombieSpawnPositions.Add(new Tuple<float, float>(newPosX, newPosZ)); //change this later
                
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

    void MoveTowards(GameObject zombie, Vector3 targetPosition)
    {
        // Use the position of the current zombie and move towards the target position
        Vector3 currentPosition = zombie.transform.position;
        Vector3 moveDirection = (targetPosition - currentPosition).normalized;

        // Ensure the zombie stops at the target position by only moving if the distance is greater than a small threshold
        float distanceToTarget = Vector3.Distance(currentPosition, targetPosition);
        if (distanceToTarget > 0.1f) // This threshold helps prevent overshooting the target
        {
            Vector3 moveVector = moveDirection * moveSpeed * Time.deltaTime;
            // This will move the zombie towards the target position
            zombie.transform.position += moveVector;
        }
    }
}
