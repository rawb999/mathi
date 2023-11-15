using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
    
{
    public Zombie target;
    public bool hasTarget = false;
    public int health;
    public int maxHealth = 100;
    public bool dead = false;
    private float deadCooldown = 2f;
    [SerializeField] FloatingHealthbar healthBar;
    // Start is called before the first frame update
    public Animator animator;
    public float attackCooldown = 1f; // seconds
    private float attackCooldownLeft = 0f;
    private int attackDamage;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        if (collectableControl.waveNumber < 5)
        {
            attackDamage = 20;
        } else if (collectableControl.waveNumber < 10 && collectableControl.waveNumber >= 5)
        {
            attackDamage = 30;
        } else if (collectableControl.waveNumber < 15 && collectableControl.waveNumber >= 10)
        {
            attackDamage = 50;
        }


        health = maxHealth;

        healthBar.UpdateHealthBar(health, maxHealth);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateHealthBarVisibility();
        attackCooldownLeft -= Time.deltaTime;
        if (dead)
        {
            deadCooldown -= Time.deltaTime;

        }

        if (deadCooldown <= 0)
        {
            Destroy(gameObject);
        }

        if (hasTarget && !dead)
        {
           
            if (target.dead == true)
            {
                hasTarget = false;
            }
            if (target != null)
            {
                //animator.SetBool("attacking", true);
                if (attackCooldownLeft <= 0.1f)
                {
                    //animator.SetTrigger("attackTrigger");
                    attackCooldownLeft = attackCooldown;
                    target.TakeDamage(attackDamage);
                }
            }
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

    private void UpdateHealthBarVisibility()
    {
        healthBar.gameObject.SetActive(health < maxHealth && health > 0);
    }
}
