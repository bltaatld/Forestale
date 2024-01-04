using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TE_EnemyChase : MonoBehaviour
{
    [Header("Status")]
    public int enemyHealth;

    [Header("Logic Component")]
    public GameObject enemyObject;
    public PlayerController Player;

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            DeathEffect();
        }
    }

    void DeathEffect()
    {
        Destroy(enemyObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Attack"))
        {
            if(enemyHealth > 0)
            {
                enemyHealth -= 1;
                Debug.Log(gameObject + " damaged! " + "CurrentHealth= " + enemyHealth);
            }
        }

        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log(gameObject + "attacked!");
            Player.playerStatus.HP -= 1;
        }
    }
}
