using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public PlayerController playerController;
    public float currentValue;

    public void Update()
    {
        UpdateMP();
    }

    void UpdateMP()
    {
        slider.maxValue = playerController.playerMaxMP;
        slider.value = playerController.playerStatus.MP;
    }
}
