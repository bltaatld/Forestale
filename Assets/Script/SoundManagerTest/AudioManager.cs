using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioSource bgmSource;
    public AudioClip[] soundEffect;

    public float currentSFX;
    public float currentBGM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(int i)
    {
        audioSource.PlayOneShot(soundEffect[i]);
    }

    public void SetSound(float f)
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        audioSource.volume = f;
    }
    public void SetBGM(float f)
    {
        bgmSource = GameObject.Find("BGM").GetComponent<AudioSource>();
        bgmSource.volume = f;
    }
}
