using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class ArmyLogic : MonoBehaviour
{
    public static int currentZombs;
    public static int currentTankZombs;
    public static int currentMeleeZombs;
    public static int currentRangedZombs;
    public GameObject player;
    private List<Tuple<float, float>> zombieSpawnPositions = new List<Tuple<float, float>>();
    private float moveSpeed = 3;
    private float strafeSpeed = 4;
    private int updatedTankScore;
    private int updatedRangedScore;
    private int updatedMeleeScore;
    public static int recentTotalScore; // changed for testing from private int
    public static int updatedTotalScore; // same as above
    public GameObject[] prefabsToInstantiate; //0 to 38 are melee zombies, 39 is tank, 40 is ranger
    private int prefabIndex; 
    private List<float> xSpawnPoints = new List<float> { 0f, -.5f, .5f, -1f, 1f, -1.5f, 1.5f, -2f, 2f, -2.5f, 2.5f, -3f, 3f, -3.5f, 3.5f, -4f, 4f, -4.5f, 4.5f, -5f, 5f };
    private List<float> zSpawnPoints = new List<float> { -2f, -3f, -4f, -5f, -6f, -7f , -8f, -9f, -10f, -11f, -12f, -13f, -14f, -15f};
    public Animator playerAnimator;
    private List<string> animations = new List<string> { "attack", "attack2", "spellcast"};
    public static bool inFight = false;
    public float healthRechargeCooldown = 6f;
    public static float updateArmyCooldown = 1f;
    public static string recentType;
    public static bool waveStarted = false;
    public float avoidanceThreshold = 1f; 
    public float maxAvoidanceForce = .5f; 
    public float separationThreshold = 1f;
    public float separationSpeed = .5f;


    // Start is called before the first frame update
    void Start()
    {
        Reset.resetValues();
        updateZombs();
    }

    // Update is called once per frame
    void Update()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();

        if (!inFight)
        {
            healthRechargeCooldown -= Time.deltaTime;
            updateArmyCooldown -= Time.deltaTime;
            

            if (updateArmyCooldown < 0) // called every time the score changes to see if we need to add zombies
            {
                
                updatedTotalScore = collectableControl.totalScoreCount;
                if (updatedTotalScore != recentTotalScore)
                {
                    recentTotalScore = updatedTotalScore;
                    playerAnimator.SetTrigger(animations[UnityEngine.Random.Range(0, 3)]);
                    updateZombs();
                    resetArmy();
                }
            }

            foreach (Zombie zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                if (healthRechargeCooldown < 0 && zombieScript.health < zombieScript.maxHealth)
                {
                    zombieScript.health = zombieScript.maxHealth;
                }
                zombie.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World); //moves the character forward

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && zombieScript.dead == false)  //if player is not heading outside the boundary
                {
                    if (zombie.transform.position.x > (levelBoundary.leftSide + zombieScript.offset.x)) //if player is pressing left key, move left
                    {
                        zombie.transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed);
                    }
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && zombieScript.dead == false) //if player is not heading outside the boundary
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
            if (waveStarted == false)
            {
                collectableControl.waveNumber++;
                waveStarted = true;
            }

            CameraController.row = 0;
            updateArmyCooldown = 1f;
            Enemy[] enemies = FindObjectsOfType<Enemy>();
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

                if (enemyScript.hasTarget == true && enemyScript.target != null && enemyScript.dead == false) // If enemy has a target, look at that target
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
                Vector3 separationForce = CalculateSeparationForce(zombie, zombies);
                zombie.transform.Translate(separationForce * Time.deltaTime, Space.World);
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

                    if (zombieScript.hasTarget == true && zombieScript.target != null && zombieScript.dead == false)
                    {
                        // Direction to target
                        Vector3 targetPosition = zombieScript.target.transform.position;
                        Vector3 directionToTarget = (targetPosition - zombie.transform.position).normalized;

                        // Calculate stopping position
                        Vector3 stopPosition = targetPosition - directionToTarget * (zombieScript.attackRange - 0.1f);
                        float distanceToTarget = Vector3.Distance(zombie.transform.position, targetPosition);


                        // Obstacle avoidance
                        Vector3 avoidanceVector = CalculateAvoidanceVector(zombie, zombies);

                        // Combine all vectors
                        Vector3 finalDirection = directionToTarget + separationForce + avoidanceVector;
                        finalDirection.Normalize();

                        // Check if zombie needs to stop
                        if (distanceToTarget > zombieScript.attackRange)
                        {
                            // Move zombie towards the stop position
                            Vector3 newPosition = Vector3.MoveTowards(zombie.transform.position, stopPosition, moveSpeed * Time.deltaTime);
                            zombie.transform.position = newPosition;
                        }

                        // Rotate the zombie to face the target
                        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                        zombie.transform.rotation = Quaternion.Slerp(zombie.transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }



    private Vector3 CalculateAvoidanceVector(Zombie currentZombie, Zombie[] allZombies)
    {
        Vector3 avoidanceVector = Vector3.zero;

        foreach (Zombie otherZombie in allZombies)
        {
            if (otherZombie != currentZombie)
            {
                float distance = Vector3.Distance(currentZombie.transform.position, otherZombie.transform.position);
                if (distance < avoidanceThreshold)
                {
                    Vector3 awayFromZombie = currentZombie.transform.position - otherZombie.transform.position;
                    avoidanceVector += awayFromZombie.normalized / distance;
                }
            }
        }

        return avoidanceVector.normalized * maxAvoidanceForce;
    }


    private Vector3 CalculateSeparationForce(Zombie currentZombie, Zombie[] allZombies)
    {
        Vector3 separationForce = Vector3.zero;
        int nearbyZombiesCount = 0;

        foreach (Zombie otherZombie in allZombies)
        {
            if (otherZombie != currentZombie)
            {
                float distance = Vector3.Distance(currentZombie.transform.position, otherZombie.transform.position);
                if (distance < separationThreshold)
                {
                    Vector3 awayFromZombie = currentZombie.transform.position - otherZombie.transform.position;
                    separationForce += awayFromZombie.normalized / distance; 
                    nearbyZombiesCount++;
                }
            }
        }

        if (nearbyZombiesCount > 0)
        {
            separationForce /= nearbyZombiesCount; 
            separationForce *= separationSpeed;
        }

        return separationForce;
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
            waveStarted = false;
            Zombie[] zombies = FindObjectsOfType<Zombie>();
            resetArmy();
            fight.endFight();

        }
    }

    private int resetPositions(string type, Zombie[] zombies, int counter)
    {
        int row;
        foreach (Zombie zombie in zombies)
        {
            if (zombie != null)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                if (zombie.gameObject.activeSelf && zombieScript.dead == false && zombieScript.type == type)
                {
                    zombieScript.hasTarget = false;
                    row = counter / 21;
                    Vector3 originalPosition = new Vector3(xSpawnPoints[counter - (21 * row)] + player.transform.position.x, .5f, zSpawnPoints[row] + player.transform.position.z);
                    zombie.transform.position = originalPosition;
                    zombie.transform.rotation = Quaternion.identity;
                    Vector3 offset = zombie.transform.position - player.transform.position;
                    zombieScript.offset = offset;
                    counter++;

                    CameraController.row = Mathf.Min(row, 4);
                }
            }
            
        }
        return counter;
    }

    private void resetArmy()
    {
        healthRechargeCooldown = 2f;
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        int counter = 0;
        counter = resetPositions("tank", zombies, counter);
        counter = resetPositions("melee", zombies, counter);
        counter = resetPositions("ranged", zombies, counter);
    }

    public void updateZombs()
    {
        int neededZombs = updatedTotalScore / 10;
        int neededTankZombs = updatedTankScore / 10;
        int neededMeleeZombs = updatedMeleeScore / 10;
        int neededRangedZombs = updatedRangedScore / 10;
        //if the # of spawned zombies is not equal to how many there should be
        while ((currentZombs < neededZombs) && (currentZombs < 210))
        {
            if (recentType == "melee")
            {
                currentMeleeZombs++;
                prefabIndex = UnityEngine.Random.Range(0, 39);
            }

            if (recentType == "ranged")
            {
                currentRangedZombs++;
                prefabIndex = 40;
            }

            if (recentType == "tank")
            {
                currentTankZombs++;
                prefabIndex = 39;
            }
            
            prefabsToInstantiate[prefabIndex].SetActive(false); //set the prefab to inactive first
            int row = currentZombs / 21;
            CameraController.row = Mathf.Min(row, 4);
            //spawn the zombie in its proper position based on its row and column
            Vector3 spawnPosition = new Vector3(player.transform.position.x + xSpawnPoints[currentZombs - (21 * row)], .5f, player.transform.position.z + zSpawnPoints[row]);
            Quaternion spawnRotation = Quaternion.identity; // No rotation

            // Instantiate the prefab
            GameObject zombie = Instantiate(prefabsToInstantiate[prefabIndex], spawnPosition, spawnRotation); //instantiate the zombie
            Vector3 offset = zombie.transform.position - player.transform.position;
            Zombie zombieScript = zombie.GetComponent<Zombie>();
            zombieScript.offset = offset;
            zombieScript.type = recentType;
            prefabsToInstantiate[prefabIndex].SetActive(true); //set the zombie back to active now that the type is set
            zombie.SetActive(true);
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
