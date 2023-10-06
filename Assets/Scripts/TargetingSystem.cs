using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{
    public LayerMask pushableObjects;
    public int numberOfTargetsWithinRange;
    public List<GameObject> m_CandidateTargets;

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
            ++numberOfTargetsWithinRange;

            m_CandidateTargets.Add(other.gameObject);
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
                --numberOfTargetsWithinRange;

                m_CandidateTargets.Remove(other.gameObject);
            }
            else
            {
                Debug.Log("Not in Layermask");
            }
    }
}

