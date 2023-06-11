using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
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

        Debug.Log("P1 takes damage");
        animator.SetTrigger("hurt");
        currentHealth -= damage;
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

        this.enabled = false;
    }

}
