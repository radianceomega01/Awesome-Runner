using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    
    Transform playerTransform;

    [SerializeField]
    GameObject deathRingPrefab;

    float distanceToStartMoving = 20f;

    [SerializeField]
    Transform monsterBullet;

    float monsterVelocity;
    bool moveLeft;
    float minVelocity = 1f;
    float maxVelocity = 2f;

    bool isPlayerInRegion;
    string FUNCTION_TO_INVOKE = "MonsterShoot";
    // Start is called before the first frame update
    void Start()
    {
        monsterVelocity = Random.Range(minVelocity, maxVelocity);
        if (Random.Range(0f, 1f) < 0.5f)
        {
            moveLeft = true;
        }
        else {
            moveLeft = false;
        }

        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("reached");
        if (playerTransform)
        {
            
            if (transform.position.x - playerTransform.position.x < distanceToStartMoving)
            {
                if (moveLeft)
                {
                    Vector3 temp = transform.position;
                    temp.x -= Time.deltaTime * monsterVelocity;
                    transform.position = temp;
                }
                else
                {
                    Vector3 temp = transform.position;
                    temp.x += Time.deltaTime * monsterVelocity;
                    transform.position = temp;
                }

                if (!isPlayerInRegion)
                {
                    InvokeRepeating(FUNCTION_TO_INVOKE, 0.5f, 1f);
                    isPlayerInRegion = true;
                }
            }
            else
            {
                CancelInvoke(FUNCTION_TO_INVOKE);
            }

        }
    }

    void MonsterShoot()
    {
        //monsterBullet = GameObject.FindGameObjectWithTag(Tags.MONSTER_BULLET_TAG).transform;
        Vector3 bulletPosition = transform.position;
        bulletPosition.y += 1.5f;
        bulletPosition.x -= 1f;
        Transform bullet = Instantiate(monsterBullet, bulletPosition, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(Vector3.left * 500f);
        bullet.parent = transform;
    }

    void MonsterDied()
    {
        Vector3 pos = transform.position;
        pos.y += 1f;
        Instantiate(deathRingPrefab, pos, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == Tags.PLAYER_BULLET_TAG)
        {
            MonsterDied();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == Tags.PLAYER_TAG)
        {
            MonsterDied();
        }
    }
}
