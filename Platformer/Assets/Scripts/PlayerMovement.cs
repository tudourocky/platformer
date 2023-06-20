using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour {

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    public KeyCode attack;
    public KeyCode dash;

    public Animator animator;
    Player2Movement player2Movement;
    [SerializeField] GameObject player2;
    float player2posX;

    //Attack
    //private float delay = 0.01f;
    private bool attacking;
    //WASD
    public float horizontal;
    private float horizontal2;
    public float speed;
    public float baseSpeed;
    public float speed2;
    private float jumpPower = 25f;
    private bool isFacingRight = true;

    //Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 28f;
    private float dashTime = 0.2f;
    private float dashCoolDown = 0.5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private float knockBackPower = 28f;
    private float currHealth = 100;

    private void Awake()
    {
        player2Movement = player2.GetComponent<Player2Movement>();
    }
    // Update is called once per frame
    void Update() {
        if(transform.position.y < -40)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
        if (isDashing)
        {
            return;
        }
        
        if (GetComponent<stats>().healthBar.getCurrHealth() < currHealth)
        {
            currHealth = GetComponent<stats>().healthBar.getCurrHealth();
            player2posX = player2Movement.transform.position.x;
            StartCoroutine(Dash2());
        } else
        {
            currHealth = GetComponent<stats>().healthBar.getCurrHealth();
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("horizontal", horizontal);
        
        if(horizontal != 0) {
            animator.SetBool("isIdle",false);
        } else {
            animator.SetBool("isIdle",true);
        }
        
        if (Input.GetKeyDown(down))
        {
            animator.SetTrigger("crouch");
        }
        if(Input.GetKeyDown(up) && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        
        if(Input.GetKeyUp(up) && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
        if(Input.GetKeyDown(dash) && canDash) {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate() {
        if(isDashing) {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    public void Flip() {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
    //Dash2 for knockback
    public IEnumerator Dash2()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if(transform.position.x < player2posX && isFacingRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * -knockBackPower, 0f);
        } else if(transform.position.x > player2posX && !isFacingRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * -knockBackPower, 0f);
        } else
        {
            rb.velocity = new Vector2(transform.localScale.x * knockBackPower, 0f);
        }
        
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
    public void speedBoost()
    {
        speed += 3f;
        //await Task.Delay((int)(5000));
        //speed = baseSpeed;
    }
    /*
    private void Attack() {
        if(attacking) {
            return;
        }
        animator.SetBool("attack", true);
        attacking = true;
        StartCoroutine(DelayAttack());
    }
    private IEnumerator DelayAttack() {
        yield return new WaitForSeconds(delay);
        attacking = false;
        animator.SetBool("attack",false);
    }
    */
    public bool getIsFacingRight()
    {
        return isFacingRight;
    }
}
