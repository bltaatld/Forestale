using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CamFovChanger : MonoBehaviour
{
    private int currentValue
    {
        get => _currentValue;
        set {
            
            _currentValue = value;
            pixelPerfectCamera.assetsPPU = currentValue;
        }
    }
    private int _currentValue;
    private int targetValue;
    public int fov;
    public PixelPerfectCamera pixelPerfectCamera;
    private float timer = 0f;
    public float valueUpdateInterval;

    private void Start()
    {
        targetValue = pixelPerfectCamera.assetsPPU;
        currentValue = targetValue;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            if (currentValue < targetValue)
            {
                currentValue++;
                timer = valueUpdateInterval;
            }
            else if (currentValue > targetValue)
            {
                currentValue--;
                timer = valueUpdateInterval;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetValue = fov;
        }
    }
}