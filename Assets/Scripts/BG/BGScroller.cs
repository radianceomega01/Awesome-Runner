using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject background;
    float offset = 0.0015f;
    [HideInInspector]
    public bool canScroll;
    Renderer renderer;
    void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canScroll)
        {
            renderer.material.mainTextureOffset += new Vector2(offset, 0f);
        }
    }

}
