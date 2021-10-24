using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector: MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == Tags.PLATFORM_TAG || collider.tag == Tags.MONSTER_TAG || collider.tag == Tags.COLLECTABLE_TAG)
        {
            collider.gameObject.SetActive(false);
        }
    }
}
