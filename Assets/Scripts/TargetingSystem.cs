using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{
    public LayerMask pushableObjects;
    public int numberOfTargetsWithinRange;
    public IList candidateTargets;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((pushableObjects.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Debug.Log("Hit with Layermask");
            candidateTargets.Add(other.gameObject);

            ++numberOfTargetsWithinRange;
        }
        else
        {
            Debug.Log("Not in Layermask");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            if ((pushableObjects.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                Debug.Log("Hit with Layermask");
                candidateTargets.Remove(other.gameObject);

                --numberOfTargetsWithinRange;
            }
            else
            {
                Debug.Log("Not in Layermask");
            }
    }
}

