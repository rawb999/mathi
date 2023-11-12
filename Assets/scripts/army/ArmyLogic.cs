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
    private List<float> zSpawnPoints = new List<float> { -2f, -3f, -4f, -5f };
    private int row = 0;
    public Animator playerAnimator;
    private List<string> animations = new List<string> { "attack", "attack2", "spellcast"};
    public static bool inFight = false;
    public float stoppingDistance = 1.0f; // This is the distance at which the zombie will stop from the enemy
    private Dictionary<GameObject, Vector3> originalOffsets;
    public bool fightStarted = false;
    public Enemy[] allEnemies = new Enemy[0];


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
            recentScore = updatedScore;
            playerAnimator.SetTrigger(animations[UnityEngine.Random.Range(0, 3)]);
            updateZombs();
        }
        if (!inFight)
        {
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
            checkEnemiesCount(); //checks to see if enemies are still alive. if not, fight ends.


            foreach (GameObject zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();

                if (!zombieScript.hasTarget) //if the zombie does not have a target
                {
                    Enemy nearestEnemy = FindNearestEnemy(zombie); //attempt the find the closest target 
                    if (nearestEnemy != null) //if target found, assign it to the zombie
                    {
                        zombieScript.target = nearestEnemy;
                        zombieScript.hasTarget = true;
                    }
                }

                if (zombieScript.hasTarget == true && zombieScript.target != null) // If zombie has a target, move towards that target
                {
                    float step = moveSpeed * Time.deltaTime;
                    Vector3 targetPosition = zombieScript.target.transform.position;

                    // Calculate a position that is 1 unit away from the target
                    Vector3 stopPosition = targetPosition - (targetPosition - zombie.transform.position).normalized;

                    // Use Vector3.MoveTowards to move towards the stop position
                    Vector3 newPosition = Vector3.MoveTowards(zombie.transform.position, stopPosition, step);

                    // Apply the new position to the zombie
                    zombie.transform.position = newPosition;
                }
            }

            
        }

    }

    public void checkEnemiesCount() //checks to see if all enemies are dead
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        bool stillSomeAlive = false;
        foreach (Enemy enemy in allEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemy != null && enemy.gameObject.activeSelf && enemyScript.dead == false)
            {
                stillSomeAlive = true;
            }
        }

        if (!stillSomeAlive)
        {
            foreach (GameObject zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                zombieScript.hasTarget = false;
                MoveToOriginalPosition(zombie);
            }
            fight.endFight();
        }

        /* leaving here until I test and make sure it's unnecessary
        if (allEnemies.Length == 0)
        {
            foreach (GameObject zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                zombieScript.hasTarget = false;
                MoveToOriginalPosition(zombie);
            }
            fight.endFight();
        }*/
    }




    private void MoveToOriginalPosition(GameObject zombie)
    {
        
        if (originalOffsets.TryGetValue(zombie, out Vector3 offset))
        {
            Vector3 originalPosition = offset + player.transform.position;
            zombie.transform.position = originalPosition;
            
        }
        
        
    }

    public void updateZombs()
    {

        zombCount = collectableControl.scoreCount / 10;

        //caps out at 84 zombies
        if (zombCount > 84)
        {
            zombCount = 84;
        }
        //if the # of spawned zombies is not equal to how many there should be
        while (currentZombs != zombCount)
        {
            if (currentZombs < zombCount)
            {
                currentZombs++;
                prefabIndex = UnityEngine.Random.Range(0, 39);
                if (currentZombs <= 21)
                {
                    row = 0; // 1st row
                }
                else if (currentZombs > 21 && currentZombs <= 42)
                {
                    row = 1; // 2nd row
                }
                else if (currentZombs > 42 && currentZombs <= 63)
                {
                    row = 2; // 3rd row
                }
                else
                {
                    row = 3; // 4th row
                }
                CameraController.row = row;
                float newPosX = xSpawnPoints[currentZombs - (21 * row) - 1];
                float newPosZ = player.transform.position.z + zSpawnPoints[row];
                //spawn the zombie in its proper position based on its row and column
                Vector3 spawnPosition = new Vector3(player.transform.position.x + xSpawnPoints[currentZombs - (21 * row) - 1], .5f, player.transform.position.z + zSpawnPoints[row]);
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
                    //might cause memory leak because not removing offset. fix later
                }
                currentZombs--;

                if (currentZombs <= 21)
                {
                    row = 0; // 1st row
                }
                else if (currentZombs > 21 && currentZombs <= 42)
                {
                    row = 1; // 2nd row
                }
                else if (currentZombs > 42 && currentZombs <= 63)
                {
                    row = 2; // 3rd row
                }
                else
                {
                    row = 3; // 4th row
                }
                CameraController.row = row;
            }
        }

        

    }

    private Enemy FindNearestEnemy(GameObject zombie)
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        float minDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach (Enemy enemy in allEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemy != null && enemy.gameObject.activeSelf && enemyScript.dead == false)
            {
                float distance = Vector3.Distance(zombie.transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }



}
