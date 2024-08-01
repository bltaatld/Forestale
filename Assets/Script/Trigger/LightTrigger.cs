using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightTrigger : MonoBehaviour
{
    public Light2D globalLight;
    public float newIntensity = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetGlobalLightIntensity(newIntensity);
        }
    }

    private void SetGlobalLightIntensity(float intensity)
    {
        if (globalLight != null)
        {
            globalLight.intensity = intensity;
        }
    }
}
