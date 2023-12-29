using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D rb;
    public Wall wall;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoxGoal"))
        {
            Debug.Log("Ramte trigger");
            wall.destroyWall();
            Destroy(gameObject);
        }
    }
}
