using UnityEngine;

public class PlayerPush : MonoBehaviour
{

    Vector2 lookDirection;
    Vector2 playerPosition;
    Vector2 pushDirection;

    public float pushForce;
    public LayerMask pushableObjects;

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            pushDirection = (lookDirection - playerPosition).normalized;
            RaycastHit2D hit = Physics2D.Raycast(playerPosition, pushDirection, Mathf.Infinity, pushableObjects);

            if (hit.collider != null)
            {
                print("CollisionHit");
                Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.AddForce(lookDirection * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}