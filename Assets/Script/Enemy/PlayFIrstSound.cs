using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFIrstSound : MonoBehaviour
{
    public string targetObjectName = "AudioManager";
    public AudioClip targetBGM;
    private AudioSource bgmsource;

    void Start()
    {
        // 타겟 오브젝트를 찾음
        GameObject targetObject = GameObject.Find(targetObjectName);

        if (targetObject != null)
        {
            foreach (Transform child in targetObject.transform)
            {
                AudioSource audioSource = child.GetComponent<AudioSource>();

                if (audioSource != null)
                {
                    Debug.Log("Found audio source object inside target object: " + child.gameObject.name);
                    bgmsource = child.GetComponent<AudioSource>();
                }
            }
        }
        else
        {
            Debug.Log("Target object with name " + targetObjectName + " not found.");
        }

        if (bgmsource.clip != targetBGM)
        {
            bgmsource.clip = targetBGM;
            bgmsource.Play();
        }
    }
}
