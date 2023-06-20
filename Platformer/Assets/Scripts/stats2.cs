using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats2 : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar2 healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void takeDamage(int damage)
    {
        Debug.Log("P2 takes damage");
        animator.SetTrigger("hurt");
        FindObjectOfType<AudioManager>().Play("HurtSound");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        GetComponent<PlayerCombat2>().updateHealTime();
        if(currentHealth <= 0)
        {
            die();
        }
    }
    public void heal(int healAmount)
    {
        if (currentHealth == maxHealth) return;
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
    void die()
    {
        Debug.Log("P2 died");
        animator.SetBool("isDead", true);
        //GetComponent<Collider2D>().enabled = false;
        GetComponent<Player2Movement>().enabled = false;
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        FindObjectOfType<GameManager>().EndGameOne();
        this.enabled = false;
    }
    
}
