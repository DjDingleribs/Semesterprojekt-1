using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyPatrole2 : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;
    public float speed;
    public bool walk = true;
    public Shake Shake;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointB.transform;
        animator.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform && walk == true)
            {
                rb.velocity = new Vector2(speed, 0.1f);
            }
            else if (walk == true)
            {
                rb.velocity = new Vector2(-speed, 0.1f);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                Flip();
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                Flip();
                currentPoint = pointB.transform;
            }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            Shake.start = true;
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
