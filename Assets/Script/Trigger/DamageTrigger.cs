using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public int enemyDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().playerStatus.HP -= enemyDamage;
            collision.GetComponent<PlayerController>().PlayerStatusCheck();
        }
    }
}
