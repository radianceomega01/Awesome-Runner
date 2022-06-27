using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int levelLength;

    [SerializeField]
    private int platformStartLength = 3, platformEndLength = 5;

    [SerializeField]
    private int platFormDistance = 5;

    [SerializeField]
    private float platformHeightMin = 0f, platformHeightMax = 10f;

    [SerializeField]
    private Transform PlatformPrefab, platformParent;

    [SerializeField]
    private Transform monsterParent, monsterPrefab;

    [SerializeField]
    private Transform collectableParent, collectablePrefab;

    [SerializeField]
    private float collectableHeightMin = 1f, collectableHeightMax = 4f;

    [SerializeField]
    private float probabMoster = 0.5f, probabCollectable = 0.1f;

    [SerializeField]
    private int platformLengthMin = 2, platformLengthMax = 4;

    [SerializeField]
    private float platformLastpositionX;

    private bool firstTimePlatform;

    void Start()
    {
        GenerateLevel(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    enum PlatformType
    {
        None,
        Flat
    }
    class PlatformInfo
    {
        public PlatformType platformType;
        public bool hasCollectable;
        public bool hasMonster;
        public float positionY;

        public PlatformInfo(PlatformType platformType, bool hasCollectable, bool hasMonster, float positionY)
        {
            this.platformType = platformType;
            this.hasCollectable = hasCollectable;
            this.hasMonster = hasMonster;
            this.positionY = positionY;
        }
    }

    void FillPlatformInfo(PlatformInfo[] platformInfo)
    {
        int currentPlatformInfoIndex = 0;

        for (int i = 0; i < platformStartLength; i++)
        {
            platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
            platformInfo[currentPlatformInfoIndex].positionY = 0f;

            currentPlatformInfoIndex++;
        }

        while (currentPlatformInfoIndex < levelLength - platformEndLength)
        {
            /*if (platformInfo[currentPlatformInfoIndex -1].platformType != PlatformType.None)
            {
                currentPlatformInfoIndex++;
                continue;
            }*/

            float platformPositionY = Random.Range(platformHeightMin, platformHeightMax);

            int platformLength = Random.Range(platformLengthMin, platformLengthMax);

            for (int i = 0; i < platformLength; i++)
            {
                bool has_Monster = (Random.Range(0f, 1f) < probabMoster);
                bool has_healthCollectable = (Random.Range(0f, 1f) < probabCollectable);

                platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = platformPositionY;
                platformInfo[currentPlatformInfoIndex].hasMonster = has_Monster;
                platformInfo[currentPlatformInfoIndex].hasCollectable = has_healthCollectable;

                currentPlatformInfoIndex++;

                if (currentPlatformInfoIndex > (levelLength - platformEndLength))
                {
                    currentPlatformInfoIndex = levelLength - platformEndLength;
                    break;
                }

            }

            for (int i = 0; i < platformEndLength; i++)
            {
                platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = 0f;

                currentPlatformInfoIndex++;
            }

        }
    }

    void createPlatform(PlatformInfo[] platforminfo, bool firstTimePlatform)
    {
        for (int i = 0; i < platforminfo.Length; i++)
        {
            if (platforminfo[i].platformType == PlatformType.None)
            {
                continue;
            }
            Vector3 posVector;
            if (firstTimePlatform)
            {
                posVector = new Vector3(platFormDistance * i, platforminfo[i].positionY, 0);
            }
            else {
                posVector = new Vector3(platFormDistance + platformLastpositionX, platforminfo[i].positionY, 0);
            }
            platformLastpositionX = posVector.x;

            if (platforminfo[i].hasMonster)
            {
                if (firstTimePlatform)
                {
                    posVector = new Vector3(platFormDistance * i, platforminfo[i].positionY + 0.1f,0);
                }
                else
                {
                    posVector = new Vector3(platFormDistance + platformLastpositionX, platforminfo[i].positionY + 0.1f, 0);
                }
                Transform monsterTransform = Instantiate(monsterPrefab, posVector, Quaternion.Euler(0,-90,0));
                monsterTransform.parent = monsterParent;
            }
            Transform platformTransform = Instantiate(PlatformPrefab, posVector, Quaternion.identity);
            platformTransform.gameObject.AddComponent<BoxCollider>();
            platformTransform.gameObject.layer = LayerMask.NameToLayer(Tags.PLATFORM_TAG);
            platformTransform.gameObject.tag = Tags.PLATFORM_TAG;
            platformTransform.parent = platformParent;
        }

    }

    public void GenerateLevel(bool firstTimePlatform)
    {
        PlatformInfo[] platformInfo = new PlatformInfo[levelLength];
        for (int i = 0; i < platformInfo.Length; i++)
        {
            platformInfo[i] = new PlatformInfo(PlatformType.None, false, false, -1f);
        }

        FillPlatformInfo(platformInfo);
        createPlatform(platformInfo, firstTimePlatform);

    }
}


