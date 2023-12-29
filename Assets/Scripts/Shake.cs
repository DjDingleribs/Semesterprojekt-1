using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 0.2f;

    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    public IEnumerator Shaking()
    {
        Debug.Log("Shake mig!");
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere;
            yield return null;
        }

        transform.position = startPos;
    }
}
