using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    // Start is called before the first frame update
    float timeToDisapper = 0.5f;
    float constY;
    void Start()
    {
        constY = transform.position.y;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, constY, transform.position.z);
        
    }
    IEnumerator RemoveBullet()
    {
        yield return new WaitForSeconds(timeToDisapper);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == Tags.MONSTER_TAG || collider.tag == Tags.PLAYER_TAG || collider.tag == Tags.MONSTER_BULLET_TAG || collider.tag == Tags.PLAYER_BULLET_TAG)
        {
            gameObject.SetActive(false);
        }
    }
}
