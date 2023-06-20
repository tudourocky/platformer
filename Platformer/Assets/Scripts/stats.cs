using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    
    public void takeDamage(int damage)
    {
        Debug.Log("P1 takes damage");
        animator.SetTrigger("hurt");
        // play hurt sound effect
        FindObjectOfType<AudioManager>().Play("HurtSound");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        GetComponent<PlayerCombat>().updateHealTime();
        if (currentHealth <= 0)
        {
            die();
        }
    }
    public void heal(int healAmount)
    {
        if (currentHealth == maxHealth) return;
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
    void die()
    {
        Debug.Log("P1 died");
        animator.SetBool("isDead", true);
        //GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        // play death sound effect
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        FindObjectOfType<GameManager>().EndGame();
        this.enabled = false;
    }


}
