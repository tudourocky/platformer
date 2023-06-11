using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerCombat2 : MonoBehaviour
{
    public KeyCode attack;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.3f;
    public LayerMask enemyLayers;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(attack))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if(animator.GetBool("isDead"))
        {
            dead();
        }
        
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        for(int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponent<stats>().takeDamage(attackDamage);

        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private async void dead()
    {
        await Task.Delay((int)(2000));
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }
}
