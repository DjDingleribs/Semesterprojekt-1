using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargets : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    private Vector3 velocity;
    public float smoothtime = .25f;

    public float minZoom = 10f;
    public float maxZoom = 5f;
    public float zoomLimiter = 30f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothtime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
