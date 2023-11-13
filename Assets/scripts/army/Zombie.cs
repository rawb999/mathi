using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int health;
    public Enemy target;
    public bool hasTarget = false;
    private const float attackDistance = 1.2f;
    public float attackCooldown = 1f; // seconds
    private float attackCooldownLeft = 0f;
    private Animator animator;
    public bool dead = false;
    public int maxHealth = 100;
    public Vector3 offset;
    [SerializeField] FloatingHealthbar healthBar;
    private float deadCooldown = 2f;

    void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        health = maxHealth;

        healthBar.UpdateHealthBar(health, maxHealth);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (dead)
        {
            deadCooldown -= Time.deltaTime;

        }

        if (deadCooldown <= 0)
        {
            ArmyLogic.currentZombs--;
            collectableControl.scoreCount -= 10;
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
            if (dead == false && Vector3.Distance(transform.position, target.transform.position) <= attackDistance && target.dead == false)
            {
                animator.SetBool("attacking", true);
                if (attackCooldownLeft <= 0.1f)
                {
                    animator.SetTrigger("attackTrigger");
                    attackCooldownLeft = attackCooldown;
                    target.TakeDamage(5);
                }
            }
        }

        if (!hasTarget || (Vector3.Distance(transform.position, target.transform.position) > attackDistance))
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

}