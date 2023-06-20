using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerCombat : MonoBehaviour
{
    // Downward slash
    public KeyCode attack;
    public int attackDamage = 20;
    public int baseAttackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    //Stab
    public KeyCode stab;
    public int stabDamage = 10;
    public int baseStabDamage = 10;
    public float stabRate = 2f;
    float nextStabTime = 0f;
    public Transform stabPoint;
    public float stabHeight = 0.5f;
    public float stabWidth = 5f;

    float nextHealTime = 0f;
    public int healAmount = 2;

    public Animator animator;
    public LayerMask enemyLayers;

    [SerializeField] GameObject player;
    Vector3 playerbaseScale;
    
    // Start is called before the first frame update
    void Start()
    {
        playerbaseScale = player.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(attack))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(stab))
            {
                Stab();
                nextAttackTime = Time.time + 1f / stabRate;
            }
        }
        if (Time.time >= nextHealTime)
        {
            player.GetComponent<stats>().heal(healAmount);
            nextHealTime = Time.time + 1f;
        }
        if(animator.GetBool("isDead"))
        {
            dead();
        }
        
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        FindObjectOfType<AudioManager>().Play("Swip");
        Collider2D[] hitEnemies =Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        for(int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponent<stats2>().takeDamage(attackDamage);
        }
    }
    void Stab()
    {
        animator.SetTrigger("stab");
        FindObjectOfType<AudioManager>().Play("Stab");
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(stabPoint.position, new Vector2(stabWidth,stabHeight), 0, enemyLayers);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponent<stats2>().takeDamage(stabDamage);
            
        }
    }
    public void updateHealTime()
    {
        nextHealTime = Time.time + 5f;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireCube(stabPoint.position, new Vector3(stabWidth, stabHeight));
    }
    private async void dead()
    {
        await Task.Delay((int)(2000));
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }
    public async void attackBoost()
    {
        player.transform.localScale *= 1.4f;
        attackDamage += 2;
        stabDamage += 2;
        await Task.Delay((int)(5000));
        player.transform.localScale = playerbaseScale;
        if (!player.GetComponent<PlayerMovement>().getIsFacingRight())
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x *= -1f;
            player.transform.localScale = localScale;
        }
        //attackDamage = baseAttackDamage;
        //stabDamage = baseStabDamage;
    }

    
}
