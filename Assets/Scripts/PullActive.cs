using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullActive : MonoBehaviour
{
    public bool disable = false;
    public bool enable = false;

    private SpriteRenderer render;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disable)
        {
            disable = false;
            SetSpriteDisabled();
        }

        if (enable)
        {
            enable = false;
            SetSpriteActive();
        }
    }
    public void SetSpriteDisabled()
    {
        render.enabled = false;
    }

    public void SetSpriteActive()
    {
        render.enabled = true;
    }
}
