using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
    
{
    public int health;
    public int maxHealth = 100;
    [SerializeField] FloatingHealthbar healthBar;
    // Start is called before the first frame update

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        health = maxHealth;

        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
          
            Die();
        }
    }

    void Die()
    {
        ArmyLogic armyLogic = FindObjectOfType<ArmyLogic>();
        //soldierAnimation.SetBool("death", true);
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }
}
