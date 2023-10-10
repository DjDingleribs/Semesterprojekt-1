using System;
using System.Collections;
using UnityEngine;

public class PlayerPull : MonoBehaviour
{
    public LayerMask pushableObjects;
    public float pushForce;

    int pullState = 0;
    Vector2 aimDir;
    Vector2 plyrPos;
    Vector2 pushDir;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pullState = 1;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pullState = 0;
        }

        if (pullState == 1)
        {
            plyrPos = transform.position;
            aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pushDir = (aimDir - plyrPos).normalized;

            RaycastHit2D hit = Physics2D.Raycast(plyrPos, pushDir, Mathf.Infinity, pushableObjects);

            if (hit.collider != null)
            {
                print("CollisionHit");
                Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    //rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                    rb.velocity = -pushDir * pushForce;
                }
            }
        }
    }
}