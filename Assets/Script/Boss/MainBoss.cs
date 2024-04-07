using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBoss : MonoBehaviour
{
    public GameObject bossUI;
    public TE_EnemyChase enemyChase;
    public Slider healthSilder;

    private void Update()
    {
        healthSilder.maxValue = enemyChase.enemyStatus.Health;
        healthSilder.value = enemyChase.enemyHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossUI.SetActive(false);
        }
    }
}
