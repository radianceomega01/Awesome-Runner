using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSmoke : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject smokePosition;

    [SerializeField]
    GameObject smokeEffect;

    private void Update()
    {
        //Debug.Log("activated");
    }
    private void OnTriggerEnter(Collider collider)
    {
       // Debug.Log("activated");
        if (collider.tag == Tags.PLATFORM_TAG)
        {
            
            if (smokePosition.activeInHierarchy)
            {
                
                Instantiate(smokeEffect, smokePosition.transform.position, Quaternion.identity);
            }
        }
    }
}
