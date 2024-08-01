using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightUp : MonoBehaviour
{
    public Light2D targetLight;
    private bool isActive = false;

    void Update()
    {
        if(targetLight.intensity < 0.7f && isActive)
        {
            targetLight.intensity += 0.001f;
        }
    }

    public void ActiveLightUp()
    {
        isActive = true;
    }
}
