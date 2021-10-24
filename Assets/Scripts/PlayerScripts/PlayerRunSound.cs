using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunSound : MonoBehaviour
{
    // Start is called before the first frame update
    float audioFootVolume = 0.4f;
    float pitchRandomness = 0.1f;
    AudioSource audioSource;

    [SerializeField]
    AudioClip genericFootstep;

    [SerializeField]
    AudioClip metalFootstep;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void GenerateFootSound()
    {
        audioSource.volume = audioFootVolume;
        audioSource.pitch = Random.Range(audioFootVolume - pitchRandomness, audioFootVolume + pitchRandomness);
        if (Random.Range(0f, 3f) < 2f)
        {
            audioSource.clip = genericFootstep;
        }
        else
        {
            audioSource.clip = metalFootstep;
        }
        audioSource.Play();
    }
}
