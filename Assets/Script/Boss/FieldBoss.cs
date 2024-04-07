using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldBoss : MonoBehaviour
{
    public TE_EnemyChase enemyChase;
    public Slider healthSilder;

    private void Update()
    {
        healthSilder.maxValue = enemyChase.enemyStatus.Health;
        healthSilder.value = enemyChase.enemyHealth;
    }
}
