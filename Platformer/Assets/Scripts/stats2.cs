using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats2 : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        Debug.Log("P2 takes damage");
        animator.SetTrigger("hurt");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            die();
        }
    }
    void die()
    {
        Debug.Log("P2 died");
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Player2Movement>().enabled = false;
        
        this.enabled = false;
    }
    
}
