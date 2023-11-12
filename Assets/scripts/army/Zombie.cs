using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int health = 100;
    public Enemy target;
    public bool IsMoving = false;
    public bool inRange = false; // set to true by army logic script when u want to begin attacking
    public bool hasTarget = false;
    private const float attackDistance = 1.2f;
    public float attackCooldown = 1f; // seconds
    private float attackCooldownLeft = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
            if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance && target != null)
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
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject); // Destroy the zombie GameObject
    }

}