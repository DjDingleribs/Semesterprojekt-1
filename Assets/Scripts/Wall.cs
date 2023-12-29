using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private SpriteRenderer render;
    private BoxCollider2D boxCollider;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void destroyWall()
    {
        SpriteRenderer.Destroy(render);
        boxCollider.gameObject.SetActive(false);
    }
}