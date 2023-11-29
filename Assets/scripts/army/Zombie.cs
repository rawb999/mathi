using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public int health;
    public Enemy target;
    public bool hasTarget = false;
    public float attackCooldown; // seconds
    private float attackCooldownLeft = 0f;
    private Animator animator;
    public bool dead = false;
    public int maxHealth;
    public Vector3 offset;
    [SerializeField] FloatingHealthbar healthBar;
    private float deadCooldown = 2f;
    public string type = "melee"; //set to melee as default, switched to something else by armylogic
    private int attackDamage;
    public float attackRange;
    AudioSource attackSound; 

    void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        SetStatsBasedOnType();
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        attackSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        SetStatsBasedOnType();

        if (dead)
        {
            deadCooldown -= Time.deltaTime;

        }

        if (deadCooldown <= 0)
        {
            ArmyLogic.currentZombs--;
            armyLogicSubtractZomb();
            collectableControl.totalScoreCount -= 10;
            if (type == "ranged")
            {
                collectableControl.rangedScoreCount -= 10;
            }
            else if (type == "melee")
            {
                collectableControl.meleeScoreCount -= 10;
            }
            else
            {
                collectableControl.tankScoreCount -= 10;
            }
            Destroy(gameObject);
        }
        UpdateHealthBarVisibility();
        attackCooldownLeft -= Time.deltaTime;
        if (target == null || target.Equals(null))
        {
            hasTarget = false;
        }
        if (hasTarget)
        {
            if (target.dead == true)
            {
                hasTarget = false;
            }
            if (dead == false && Vector3.Distance(transform.position, target.transform.position) <= attackRange && target.dead == false)
            {
                animator.SetBool("attacking", true);
                if (attackCooldownLeft <= 0.1f)
                {
                    attackSound.Play();
                    animator.SetTrigger("attackTrigger");
                    attackCooldownLeft = attackCooldown;
                    target.TakeDamage(attackDamage);
                }
            }
        }

        if (!hasTarget || (Vector3.Distance(transform.position, target.transform.position) > attackRange))
        {
            animator.SetBool("attacking", false);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            dead = true;
            animator.SetBool("dead", true);
        }
    }

    private void UpdateHealthBarVisibility()
    {
        healthBar.gameObject.SetActive(health < maxHealth && health > 0);
    }

    private void SetStatsBasedOnType()
    {
        switch (type)
        {
            case "melee":
                maxHealth = 100; 
                attackDamage = 5;
                attackRange = 1.2f;
                attackCooldown = 1f;
                break;
            case "tank":
                maxHealth = 200; 
                attackDamage = 2;
                attackRange = 1.2f;
                attackCooldown = 1f;
                break;
            case "ranged":
                maxHealth = 70;
                attackDamage = 4;
                attackRange = 7;
                attackCooldown = 1f;
                break;
        }
    }

    private void armyLogicSubtractZomb()
    {
        switch (type)
        {
            case "melee":
                ArmyLogic.currentMeleeZombs--;
                break;
            case "tank":
                ArmyLogic.currentTankZombs--;
                break;
            case "ranged":
                ArmyLogic.currentRangedZombs--;
                break;
        }
    }

}