using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public GameObject screenObject;
    public static readonly float FadeInTime = 0.15f;
    public static readonly float FadeOutTime = 0.25f;
    public static readonly float FadeDelayTime = 0.25f;

    [SerializeField] private Image m_image;

    private void Start()
    {
        screenObject.SetActive(false);
    }

    public void FadeIn(float time, Action endAction = null)
    {
        StartCoroutine(Fade(0, 1, time, endAction));
    }
    public void FadeOut(float time, Action endAction = null)
    {
        StartCoroutine(Fade(1, 0, time, endAction));
    }
    public void FadeInOut(float inTime, float outTime, float delay = 0.0f, Action onDelay = null, Action onEnd = null)
    {
        screenObject.SetActive(true);
        StartCoroutine(FadeDelay(inTime, outTime, delay, onDelay, onEnd));
    }

    IEnumerator FadeDelay(float inTime, float outTime, float delay, Action onDelay = null, Action onEnd = null)
    {
        yield return Fade(0, 1, inTime);

        onDelay?.Invoke();
        yield return new WaitForSeconds(delay);

        yield return Fade(1, 0, outTime);
        onEnd?.Invoke();
        screenObject.SetActive(false);
    }

    IEnumerator Fade(float start, float end, float time, Action onEnd = null)
    {
        float progress = 0.0f;
        float curTime = 0.0f;
        while (progress < 1.0f)
        {
            curTime += Time.deltaTime;
            progress = curTime / time;

            Color color = m_image.color;
            color.a = Mathf.Lerp(start, end, progress);
            m_image.color = color;

            yield return null;
        }


        onEnd?.Invoke();
    }

    public static FadeScreen GetInstance() => GameObject.FindObjectOfType<FadeScreen>(true);
}
