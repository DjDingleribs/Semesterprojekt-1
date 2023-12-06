using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    private bool isFacingRight = true;
    public bool onGround;

    public Animator animator;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public Transform pushobjCheck;
    public LayerMask pushobjLayer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetFloat("Speed", 0);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround == true)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsOnPushableObj())
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        Flip();
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    private bool IsGrounded()
    {
        Debug.Log("Er Grounded");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsOnPushableObj()
    {
        Debug.Log("På Pushable");
        return Physics2D.OverlapCircle(pushobjCheck.position, 0.2f, pushobjLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            animator.SetBool("IsJumping", false);
            onGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }

    private void Flip()
    {
        if (Input.GetKey(right) && isFacingRight || Input.GetKey(left) && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }

    }
}
