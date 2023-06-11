using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public float knockbackForce = 100f;
    public Rigidbody2D rb;
    public Collider2D collider2d;

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
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        this.enabled = false;
    }
    public void onHit(Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        Vector2 direction = (collider.transform.position - transform.position).normalized;
        Vector2 knockback = direction * knockbackForce;
        onHit(knockback);
    }

}
