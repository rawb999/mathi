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
    public float attackCooldown = 2f; // seconds
    private float attackCooldownLeft = 0f;

    void Update()
    {
        if (target == null)
        {
            hasTarget = false;
        }
        if (hasTarget)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance && target != null)
            {
                attackCooldownLeft -= Time.deltaTime;
                if (attackCooldownLeft <= 0.1f)
                {
                    
                    attackCooldownLeft = attackCooldown;
                    target.TakeDamage(10);
                }
            }
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