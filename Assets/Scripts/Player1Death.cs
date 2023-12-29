using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Death : MonoBehaviour
{
    public Shake Shake;
    private Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("IsPlayer1Dead");
        Shake.start = true;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
