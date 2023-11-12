using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
    
{
    public int health;
    public int maxHealth = 100;
    public bool dead = false;
    private float deadCooldown = 2f;
    [SerializeField] FloatingHealthbar healthBar;
    // Start is called before the first frame update
    public Animator animator;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        health = maxHealth;

        healthBar.UpdateHealthBar(health, maxHealth);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            deadCooldown -= Time.deltaTime;

        }

        if (deadCooldown <= 0)
        {
            Destroy(gameObject);
        }
    }



    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            dead = true;
            animator.SetBool("death", true);
        }
    }
}
