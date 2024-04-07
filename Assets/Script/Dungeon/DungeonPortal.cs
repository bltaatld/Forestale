using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    private MapGenerator mapGenerator;
    private FadeScreen m_fade;

    private void Start()
    {
        m_fade = FadeScreen.GetInstance();
        mapGenerator = MapGenerator.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_fade.FadeInOut(FadeScreen.FadeInTime, FadeScreen.FadeOutTime, FadeScreen.FadeDelayTime, mapGenerator.ReGenerate);
        }
    }
}
