using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : MonoBehaviour
{
    [Header("Random Pattern Time")]
    public int minValue = 0;
    public int maxValue = 3;
    public float interval = 5.0f;
    public bool isOnField;

    [Header("Reference")]
    public Animator animator;

    void Start()
    {
        InvokeRepeating("GenerateRandomValue", 0, interval);
    }

    void GenerateRandomValue()
    {
        int randomValue = Random.Range(minValue, maxValue);
        animator.SetInteger("CurrentPattern", randomValue);
    }

    public void CamEffect()
    {
        CameraShake.instance.Shakecamera(8f, 0.5f);
    }
}
