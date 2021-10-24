using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    Button soundBtn;

    [SerializeField]
    Sprite soundOn, soundOff;
    // Start is called before the first frame update
    void Start()
    {
        soundBtn = GameObject.Find("Sound").GetComponent<Button>();
    }

    public void OnPlayButtonTap()
    {
        SceneManager.LoadScene(Tags.GAMESCENE_TAG);
    }

    public void OnSoundButtonTap()
    {
        if (GameManager.instance.isSoundOn)
        {
            soundBtn.image.sprite = soundOff;
            GameManager.instance.isSoundOn = false;
        }
        else {
            soundBtn.image.sprite = soundOn;
            GameManager.instance.isSoundOn = true;
        }
    }

}
