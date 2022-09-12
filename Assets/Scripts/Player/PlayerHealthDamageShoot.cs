using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDamageShoot : MonoBehaviour
{
    // Start is called before the first frame update
    float distanceOfMorePlatforms = 90f;
    LevelGenerator levelGenerator;
    LevelPooling levelPooling;

    [SerializeField]
    Transform playerBulletPrefab;

    [SerializeField]
    GameObject deathSkullPrefab;

    [SerializeField]
    GameObject mobileInputs;

    GameplayController gamePlayController;
    Button shootBtn;
    void Awake()
    {
        levelPooling = GameObject.Find(Tags.LEVELGENERATOR_TAG).GetComponent<LevelPooling>();
        gamePlayController = GameObject.Find(Tags.GAMEPLAYCONTROLLER_TAG).GetComponent<GameplayController>();
        if (mobileInputs.activeInHierarchy)
        {
            shootBtn = GameObject.Find(Tags.SHOOTBTN_TAG).GetComponent<Button>();
            shootBtn.onClick.AddListener(() => OnShootBtnTap());
        }
        //gamePlayController = GameplayController.instance;
        //levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnShootBtnTap();

        }

        if (transform.position.y <= -8f)
        {
            PlayerDeath();
        }
    }

    void OnShootBtnTap()
    {
        Vector3 bulletPos = transform.position;
        bulletPos.x += 0.5f;
        bulletPos.y += 1.5f;
        Transform bullet = Instantiate(playerBulletPrefab, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(Vector3.right * 1500f);
        bullet.parent = transform;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == Tags.MORE_PLATFORM_TAG)
        {
            Vector3 temp = collider.transform.position;
            temp.x += distanceOfMorePlatforms;
            collider.transform.position = temp;

            //levelGenerator.GenerateLevel(false);
            levelPooling.StartLevelPooling();
            gamePlayController.IncreaseLevel();
        }
        else if (collider.tag == Tags.MONSTER_BULLET_TAG)
        {
            PlayerDeath();
        }
        else if (collider.tag == Tags.COLLECTABLE_TAG)
        {
            collider.gameObject.SetActive(false);
            gamePlayController.OnCollectableCollected();
        }
    }

    void PlayerDeath()
    {
        Vector3 effectPos = transform.position;
        effectPos.y += 1.5f;
        Instantiate(deathSkullPrefab, effectPos, Quaternion.identity);
        gameObject.SetActive(false);
        gamePlayController.OnTakeDamage();
    }
}
