using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;

    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedPlayerDied;

    [HideInInspector]
    public int score, health, level;

    [HideInInspector]
    public bool isSoundOn = true;

    // Start is called before the first frame update
    void Awake()
    {
        gameStartedFromMainMenu = true;
        CreateSingelton();
    }

    void CreateSingelton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
