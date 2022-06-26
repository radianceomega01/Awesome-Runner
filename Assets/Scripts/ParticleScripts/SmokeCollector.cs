using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCollector : MonoBehaviour
{
    // Start is called before the first frame update
    float waitTime = 1f;
    void Start()
    {
        StartCoroutine(WaitAndCollect());
    }

    IEnumerator WaitAndCollect()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
