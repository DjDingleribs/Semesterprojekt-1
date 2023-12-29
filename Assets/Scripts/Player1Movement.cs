using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    private bool isFacingRight = true;
    public bool onGround;
    public int cooldownTime = 5;
    private float nextFireTime = 0;
    public bool isRunning = false;
    public int lifeCounter = 3;
    public Vector2 boxSize;
    public float castDistance;
    public ParticleSystem dust;
    public float currentCheckpoint;
    public bool player1GoalAchieved;

    public Animator animator;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private Rigidbody2D rb;
    private bool isDead = false;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public Transform pushobjCheck;
    public LayerMask pushobjLayer;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource landSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource walkSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("IsPlayer1Spawning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && isDead == false)
        {
            isRunning = true;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            animator.SetFloat("Player1Speed", Mathf.Abs(moveSpeed));
        }
        else if (Input.GetKey(KeyCode.D) && isDead == false)
        {
            isRunning = true;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            animator.SetFloat("Player1Speed", Mathf.Abs(moveSpeed));
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) && isDead == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetFloat("Player1Speed", 0);
            walkSound.enabled = false;
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.A) && isDead == false && isGrounded())
        {
            walkSound.enabled = true;
        }

        else if (Input.GetKey(KeyCode.D) && isDead == false && isGrounded())
        {
            walkSound.enabled = true;
        }

        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) && isDead == false && isGrounded())
        {
            walkSound.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded() && isDead == false)
        {
            animator.SetBool("IsPlayer1Jumping", true);
            CreateDust();
            walkSound.enabled = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.W) && IsOnPushableObj() && isDead == false)
        {
            animator.SetBool("IsPlayer1Jumping", true);
            CreateDust();
            walkSound.enabled = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();
        }

        if (Time.time > nextFireTime)
        {
            animator.SetBool("IsPlayer1Pushing", false);
        }



        Flip();
    }

    public void Push ()
    {
        animator.SetBool("IsPlayer1Pushing", true);
        nextFireTime = Time.time + cooldownTime;
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private bool IsOnPushableObj()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, pushobjLayer))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    void CreateDust()
    {
        dust.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            animator.SetBool("IsPlayer1Jumping", false);
            onGround = true;
        }

        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            Debug.Log("Death");
            animator.SetBool("IsPlayer1Jumping", false);
            animator.SetFloat("Player1Speed", 0);
            isDead = true;
            deathSound.Play();

            if (lifeCounter > 0)
            {
                lifeCounter--;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("IsPlayer1Jumping", false);
            animator.SetFloat("Player1Speed", 0);
            isDead = true;
            deathSound.Play();
        }

        }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform.position.x;
            Debug.Log(currentCheckpoint);
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            player1GoalAchieved = true;
            Debug.Log("Mål!");
        }
    }

    private void Flip()
    {
        if (Input.GetKey(right) && isFacingRight && isDead == false || Input.GetKey(left) && !isFacingRight && isDead == false)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;

            if (isGrounded())
            {
                CreateDust();
            }
        }
    }
}
