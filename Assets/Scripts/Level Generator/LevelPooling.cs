using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPooling : MonoBehaviour
{
    [SerializeField]
    Transform platform, platformParent;

    [SerializeField]
    Transform monster, monsterParent;

    [SerializeField]
    Transform collectable, collectableParent;

    //[SerializeField]
    float platformDistance;

    [SerializeField]
    int platformMinY, platformMaxY;

    [SerializeField]
    float collectableMinY, collectableMaxY;

    [SerializeField]
    float chanceOfMonster = 0.5f, chanceOfCollectable = 0.1f;

    Transform[] platformsArray;
    int levelLength = 100;
    float platformLastPosition;

    // Start is called before the first frame update
    void Start()
    {
        platformsArray = new Transform[levelLength];
        CreateLevels();
    }

    void CreateLevels()
    {
        for (int i = 0; i < platformsArray.Length; i++)
        {
            Transform platformTransform = Instantiate(platform, Vector3.zero, Quaternion.identity);
            platformsArray[i] = platformTransform;
            float platformHeight;
            if (i < 3)
            {
                platformHeight = 0;
                platformDistance = 12;
            }
            else
            {
                platformHeight = Random.Range(platformMaxY, platformMinY);
                platformDistance = 16;
            }

            Vector3 platformPosition = new Vector3(i * platformDistance, platformHeight, 0);
            platformsArray[i].position = platformPosition;
            platformsArray[i].parent = platformParent;
            platformLastPosition = platformsArray[i].position.x;
            SpawnCollectableAndMonster(true, i, platformPosition);

        }
    }
    public void StartLevelPooling()
    {
        for (int i = 0; i < platformsArray.Length; i++)
        {
            if (!platformsArray[i].gameObject.activeInHierarchy)
            {
                platformsArray[i].gameObject.SetActive(true);
                float platformHeight = Random.Range(platformMaxY, platformMinY);
                Vector3 platformPosition = new Vector3(platformLastPosition + platformDistance, platformHeight, 0);
                platformsArray[i].position = platformPosition;
                platformsArray[i].parent = platformParent;
                platformLastPosition = platformsArray[i].position.x;

                SpawnCollectableAndMonster(false, i, platformPosition);
            }
        }
    }

    void SpawnCollectableAndMonster(bool firstTime, int i, Vector3 platfomPosition)
    {
        if (i > 2)
        {
            if (Random.Range(0f, 1f) < chanceOfMonster)
            {
                Vector3 monsterPosition;
                if (firstTime)
                {
                    monsterPosition = new Vector3(i * platformDistance, platfomPosition.y , 0);
                }
                else
                {
                    monsterPosition = new Vector3(platformLastPosition + platformDistance, platfomPosition.y, 0);
                }
                Transform monsterTransform = Instantiate(monster, monsterPosition, Quaternion.Euler(0, -90, 0));
                monsterTransform.parent = monsterParent;
            }

            if (Random.Range(0f, 1f) < chanceOfCollectable)
            {
                Vector3 collectablePOsition;
                if (firstTime)
                {
                    collectablePOsition = new Vector3(i * platformDistance, platfomPosition.y + 0.5f, 0);
                }
                else
                {
                    collectablePOsition = new Vector3(platformLastPosition + platformDistance, platfomPosition.y + Random.Range(collectableMinY, collectableMaxY), 0);
                }
                Transform collectableTransform = Instantiate(collectable, collectablePOsition, Quaternion.identity);
                collectableTransform.parent = collectableParent;
            }
            

        }
    }
}
