    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1TargettingSystem : MonoBehaviour
{
    public LayerMask pushableObjects;
    public LayerMask Player;
    public int numberOfTargetsWithinRange;
    public List<GameObject> possibleTargets;
    public GameObject currentlyPointingAt;
    public float pushForce;
    public int currentItem = 0;
    public int cooldownPush = 3;
    private float nextPushTime = 0;
    public bool hasPushed = false;
    public PushActive pushActive;
    public GameObject targetingAt;
    [SerializeField] Sprite showSprite;

    public enemyPatrole enemy;
    public enemyPatrole1 enemy1;
    public enemyPatrole2 enemy2;
    public enemyPatrole3 enemy3;

    Vector2 gamePoint;
    Vector2 plyrPos;
    Vector2 pushDir;

    // Start is called before the first frame update
    void Start()
    {
        targetingAt = GameObject.FindGameObjectWithTag("Push");
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfTargetsWithinRange == 0)
        {
            //pushActive.SetSpriteDisabled();
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentItem += 1;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                currentItem -= 1;
            }

            if (currentItem >= possibleTargets.Count)
            {
                currentItem = 0;
            }

            if (currentItem < 0)
            {
                currentItem = possibleTargets.Count - 1;
            }

            currentlyPointingAt = possibleTargets[currentItem];

            if (currentlyPointingAt != null && numberOfTargetsWithinRange > 0)
            {
                pushActive.SetSpriteActive();
                targetingAt.transform.position = currentlyPointingAt.transform.position + new Vector3(0, 1, 0);
            }

            if (Input.GetKeyDown(KeyCode.F) && Time.time > nextPushTime)
            {
                Push();
            }

            if (hasPushed == true && Time.time > nextPushTime)
            {
                hasPushed = false;
                enemy.walk = true;
                enemy1.walk = true;
                enemy2.walk = true;
                enemy3.walk = true;
                Debug.Log("Push klar igen");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((pushableObjects.value & (1 << other.transform.gameObject.layer)) > 0)
            {
            //Debug.Log("Hit with Layermask");
            ++numberOfTargetsWithinRange;

            possibleTargets.Add(other.gameObject);
            }

            else if ((Player.value & (1 << other.transform.gameObject.layer)) > 0)
            {
            //Debug.Log("Hit with Layermask");

            if (!possibleTargets.Contains(other.gameObject))
            {
                ++numberOfTargetsWithinRange;
                possibleTargets.Add(other.gameObject);
            }
            else
            {

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            if ((pushableObjects.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                //Debug.Log("Hit with Layermask");
                --numberOfTargetsWithinRange;

                possibleTargets.Remove(other.gameObject);
            }
            else if ((Player.value & (1 << other.transform.gameObject.layer)) > 0)
            {
            //Debug.Log("Hit with Layermask");
            if (possibleTargets.Contains(other.gameObject))
            {
                --numberOfTargetsWithinRange;
                possibleTargets.Remove(other.gameObject);
            }
            else
            {

            }
        }
    }

    private void Push()
    {
        plyrPos = transform.position;
        gamePoint = currentlyPointingAt.transform.position;
        pushDir = (gamePoint - plyrPos).normalized;

        if (currentlyPointingAt != null && gameObject.CompareTag("Player") == false)
        {
            Rigidbody2D rb = currentlyPointingAt.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                enemy.walk = false;
                enemy1.walk = false;
                enemy2.walk = false;
                enemy3.walk = false;
                hasPushed = true;
                rb.velocity = pushDir * pushForce;
            }
        }
        else if (currentlyPointingAt != null && gameObject.CompareTag("Player") == true)
        {
            //print("CollisionHit");
            Rigidbody2D rb = currentlyPointingAt.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Debug.Log("STOP2");
                //rb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
                rb.velocity = pushDir * pushForce;
            }
        }
        nextPushTime = Time.time + cooldownPush;
    }
}


