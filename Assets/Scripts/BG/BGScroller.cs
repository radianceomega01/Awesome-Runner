using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public bool CanScroll { get; set; }
    float offset = 0.0015f;
    new Renderer renderer;

    public static BGScroller Instance { get; set; }
    void Awake()
    {
        renderer = GetComponent<Renderer>();
        if (Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanScroll)
        {
            renderer.material.mainTextureOffset += new Vector2(offset, 0f);
        }
    }

}
