using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    Text scoreText, healthText, levelText;

    [SerializeField]
    Image pauseBtn;

    [SerializeField]
    Image pausePanel;

    [SerializeField]
    Button resumeBtn;

    [SerializeField]
    Button homeBtn;

    [SerializeField]
    AudioSource bgAudio;

    int score, health, level;

    BGScroller bgScroller;
    // Start is called before the first frame update
    bool canCountScore, pauseActive;
    private void Awake()
    {
        scoreText = GameObject.Find(Tags.SCORE_TAG).GetComponent<Text>();
        healthText = GameObject.Find(Tags.HEALTH_TAG).GetComponent<Text>();
        levelText = GameObject.Find(Tags.LEVEL_TAG).GetComponent<Text>();
        bgScroller = GameObject.Find(Tags.BACKGROUND_TAG).GetComponent<BGScroller>();
        canCountScore = true;
    }
    void Start()
    {
        OnCreateInstance();
        if (GameManager.instance.isSoundOn)
        {
            bgAudio.Play();
        }
        else
        {
            bgAudio.Stop();
        }
    }

    void OnCreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Tags.GAMESCENE_TAG)
        {
            if (GameManager.instance.gameStartedFromMainMenu)
            {
                GameManager.instance.gameStartedFromMainMenu = false;
                score = 0;
                health = 2;
                level = 1;

            }
            else if (GameManager.instance.gameRestartedPlayerDied)
            {
                GameManager.instance.gameRestartedPlayerDied = false; ;
                canCountScore = true;
                score = GameManager.instance.score;
                health = GameManager.instance.health;
                level = GameManager.instance.level;
            }

            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            levelText.text = level.ToString();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Update is called once per frame
    void Update()
    {
        IncreaseScore();  
    }

    void IncreaseScore()
    {
        if (canCountScore)
        {
            score += 1;
            scoreText.text = score.ToString();
        }
    }
    public void OnTakeDamage()
    {
        health -= 1;
        if (health >= 1)
        {
            healthText.text = health.ToString();
            GameManager.instance.gameRestartedPlayerDied = true;
            StartCoroutine(OnPlayerDied(Tags.GAMESCENE_TAG)); 
        }
        else 
        {
            healthText.text = health.ToString();
            GameManager.instance.gameRestartedPlayerDied = false;
            GameManager.instance.gameStartedFromMainMenu = true;
            StartCoroutine(OnPlayerDied(Tags.MAINMENUSCENE_TAG));
        }
    }
    IEnumerator OnPlayerDied(string scene)
    {
        bgScroller.canScroll = false;
        canCountScore = false;

        GameManager.instance.score = score;
        GameManager.instance.health = health;
        GameManager.instance.level = level;
        yield return new WaitForSecondsRealtime(1f);
       SceneManager.LoadScene(scene);
    }

    public void OnPauseBtnTap()
    {
        if (pauseActive)
        {
            pauseActive = false;
            pausePanel.gameObject.SetActive(true);
            OnPause();
        }
        else
        {
            pauseActive = true;
            pausePanel.gameObject.SetActive(false);
            OnResume();
        }
    }

    public void OnResumeBtnTap()
    {
        pauseActive = true;
        pausePanel.gameObject.SetActive(false);
        OnResume();
    }

    public void OnHomeBtnTap()
    {
        Time.timeScale = 1f;
        GameManager.instance.gameRestartedPlayerDied = false;
        GameManager.instance.gameStartedFromMainMenu = true;
        SceneManager.LoadScene(Tags.MAINMENUSCENE_TAG);
    }
    public void IncreaseLevel()
    {
        level += 1;
        levelText.text = level.ToString();
    }

    public void OnCollectableCollected()
    {
        health += 1;
        healthText.text = health.ToString();
    }

    void OnPause()
    {
        canCountScore = false;
        //bgScroller.ScrollBackground();
        Time.timeScale = 0f;
    }

    void OnResume()
    {
        canCountScore = true;
        Time.timeScale = 1f;
    }
}
