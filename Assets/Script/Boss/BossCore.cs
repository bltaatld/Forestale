using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCore : MonoBehaviour
{
    public Animator animator;
    public GameObject bossUI;
    public Slider bossHPSlider;
    public PlayerController playerStatus;
    public TriggerTracker triggerTracker;
    public float maxBossHP;
    public float bossHP;

    private void Start()
    {
        bossHPSlider.maxValue = maxBossHP;
        bossHP = maxBossHP;
    }

    void Update()
    {
        bossHPSlider.value = bossHP;
        if (triggerTracker.triggered)
        {
            bossUI.SetActive(true);
        }
        else
        {
            bossUI.SetActive(false);
        }

        if(bossHP <= 0)
        {
            Loader.LoadScene("EndScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            animator.SetTrigger("isDamaged");
            AudioManager.instance.PlaySound(8);
            bossHP -= (int)playerStatus.damagedOutput.NormalDamage;
        }
    }

    public void CamEffect()
    {
        CameraShake.instance.Shakecamera(4f, 0.2f);
    }
}
