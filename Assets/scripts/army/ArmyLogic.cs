using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class ArmyLogic : MonoBehaviour
{
    public static int currentZombs = 0;
    public GameObject player;
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
    public Animator playerAnimator;
    private List<string> animations = new List<string> { "attack", "attack2", "spellcast"};
    public static bool inFight = false;
    public float stoppingDistance = 1.0f; // This is the distance at which the zombie will stop from the enemy
    public bool fightStarted = false;




    // Start is called before the first frame update
    void Start()
    {
        prefabIndex = UnityEngine.Random.Range(0, 39);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inFight)
        {
            updatedScore = collectableControl.scoreCount;
            if (updatedScore > recentScore)
            {
                recentScore = updatedScore;
                playerAnimator.SetTrigger(animations[UnityEngine.Random.Range(0, 3)]);
                updateZombs();
            }

            Zombie[] zombies = FindObjectsOfType<Zombie>();
            int rightmostDigit = collectableControl.scoreCount % 10;
            collectableControl.scoreCount = zombies.Length * 10 + rightmostDigit;
            foreach (Zombie zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                zombie.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World); //moves the character forward

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  //if player is not heading outside the boundary
                {
                    if (zombie.transform.position.x > (levelBoundary.leftSide + zombieScript.offset.x)) //if player is pressing left key, move left
                    {
                        zombie.transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed);
                    }
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //if player is not heading outside the boundary
                {
                    if (zombie.transform.position.x < (levelBoundary.rightSide + zombieScript.offset.x)) //if player is pressing right key, move right
                    {
                        zombie.transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed * -1);
                    }
                }
            }
        }
        else
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Zombie[] zombies = FindObjectsOfType<Zombie>();
            checkEnemiesCount(enemies); //checks to see if enemies are still alive. if not, fight ends.
            foreach (Enemy enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                if (!enemyScript.hasTarget) //if the zombie does not have a target
                {
                    Zombie nearestEnemy = FindNearestZombie(enemy); //attempt the find the closest target 
                    if (nearestEnemy != null) //if target found, assign it to the zombie
                    {
                        enemy.target = nearestEnemy;
                        enemy.hasTarget = true;
                    }
                }

                if (enemyScript.hasTarget == true && enemyScript.target != null) // If enemy has a target, look at that target
                {
                    float step = moveSpeed * Time.deltaTime;
                    Vector3 targetPosition = enemyScript.target.transform.position;

                    // Calculate a position that is 1 unit away from the target
                    Vector3 directionToTarget = (targetPosition - enemy.transform.position).normalized;

                    // Rotate the enemy to face the target
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, step);
                }
            }

            foreach (Zombie zombie in zombies)
            {
                if (zombie != null && zombie.gameObject.activeSelf)
                {
                    Zombie zombieScript = zombie.GetComponent<Zombie>();

                    if (!zombieScript.hasTarget) //if the zombie does not have a target
                    {
                        Enemy nearestEnemy = FindNearestEnemy(zombie, enemies); //attempt the find the closest target 
                        if (nearestEnemy != null) //if target found, assign it to the zombie
                        {
                            zombieScript.target = nearestEnemy;
                            zombieScript.hasTarget = true;
                        }
                    }

                    if (zombieScript.hasTarget == true && zombieScript.target != null && zombieScript.dead == false) // If zombie has a target, move towards that target
                    {
                        float step = moveSpeed * Time.deltaTime;

                        Vector3 targetPosition = zombieScript.target.transform.position;

                        // Calculate a position that is 1 unit away from the target
                        Vector3 directionToTarget = (targetPosition - zombie.transform.position).normalized;
                        Vector3 stopPosition = targetPosition - directionToTarget;

                        // Use Vector3.MoveTowards to move towards the stop position
                        Vector3 newPosition = Vector3.MoveTowards(zombie.transform.position, stopPosition, step);

                        // Apply the new position to the zombie
                        zombie.transform.position = newPosition;

                        // Rotate the zombie to face the target
                        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                        zombie.transform.rotation = Quaternion.Slerp(zombie.transform.rotation, targetRotation, step);
                    }
                }
                
            }

            
        }

    }

    public void checkEnemiesCount(Enemy[] enemies) //checks to see if all enemies are dead
    {
        bool stillSomeAlive = false;
        foreach (Enemy enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemy != null && enemy.gameObject.activeSelf && enemyScript.dead == false)
            {
                stillSomeAlive = true;
            }
        }

        if (!stillSomeAlive)
        {
            Zombie[] zombies = FindObjectsOfType<Zombie>();
            MoveToOriginalPosition();
            foreach (Zombie zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                zombieScript.hasTarget = false;
            }
            fight.endFight();

        }

        /* leaving here until I test and make sure it's unnecessary
        if (enemies.Length == 0)
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



    /* leaving here in case I want to go back to old way of moving to original position
    private void MoveToOriginalPosition(Zombie zombie)
    {
        Zombie zombieScript = zombie.GetComponent<Zombie>();
        Vector3 originalPosition = new Vector3(
        zombieScript.offset.x + player.transform.position.x,
        .5f, // Set this to the appropriate ground level
        zombieScript.offset.z + player.transform.position.z
    );
        zombie.transform.position = originalPosition;
        zombie.transform.rotation = Quaternion.identity;
    }
    */

    private void MoveToOriginalPosition()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        int counter = 0;
        int row;
        foreach (Zombie zombie in zombies)
        {
            Zombie zombieScript = zombie.GetComponent<Zombie>();
            if (zombie != null && zombie.gameObject.activeSelf && zombieScript.dead == false)
            {
                
                zombieScript.hasTarget = false;
                row = counter / 21;
                Vector3 originalPosition = new Vector3(xSpawnPoints[counter - (21 * row)] + player.transform.position.x, .5f, zSpawnPoints[row] + player.transform.position.z);
                zombie.transform.position = originalPosition;
                zombie.transform.rotation = Quaternion.identity;
                Vector3 offset = zombie.transform.position - player.transform.position;
                zombieScript.offset = offset;
                counter++;
            }
        }
    }

    public void updateZombs()
    {
        int neededZombies = collectableControl.scoreCount / 10;

        //if the # of spawned zombies is not equal to how many there should be
        while ((currentZombs < neededZombies) && (currentZombs < 84))
        {
            prefabIndex = UnityEngine.Random.Range(0, 39);
            int row = currentZombs / 21;
            CameraController.row = row;
            //spawn the zombie in its proper position based on its row and column
            Vector3 spawnPosition = new Vector3(player.transform.position.x + xSpawnPoints[currentZombs - (21 * row)], .5f, player.transform.position.z + zSpawnPoints[row]);
            Quaternion spawnRotation = Quaternion.identity; // No rotation

            // Instantiate the prefab
            GameObject zombie = Instantiate(prefabsToInstantiate[prefabIndex], spawnPosition, spawnRotation); //CHECK THIS LATER MAY CAUSE BUG
            Vector3 offset = zombie.transform.position - player.transform.position;
            Zombie zombieScript = zombie.GetComponent<Zombie>();
            zombieScript.offset = offset;
            currentZombs++;

        }

        

    }

    private Enemy FindNearestEnemy(Zombie zombie, Enemy[] enemies)
    {
        float minDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach (Enemy enemy in enemies)
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

    private Zombie FindNearestZombie(Enemy enemy)
    {
        float minDistance = Mathf.Infinity;
        Zombie nearestEnemy = null;
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        foreach (Zombie zombie in zombies)
        {
            Zombie zombieScript = zombie.GetComponent<Zombie>();
            if (zombie != null && zombie.gameObject.activeSelf && zombieScript.dead == false)
            {
                float distance = Vector3.Distance(enemy.transform.position, zombie.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = zombie;
                }
            }
        }

        return nearestEnemy;
    }


}
