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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {

        Debug.Log("P1 takes damage");
        animator.SetTrigger("hurt");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            die();
        }
    }
    void die()
    {
        Debug.Log("P1 died");
        animator.SetBool("isDead", true);
        //GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        FindObjectOfType<AudioManager>().Play("PlayerDeath");

        this.enabled = false;
    }

}
