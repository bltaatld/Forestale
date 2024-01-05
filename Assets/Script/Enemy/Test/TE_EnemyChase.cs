using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TE_EnemyChase : MonoBehaviour
{
    [Header("Status")]
    public EnemyStatus enemyStatus;
    private int m_enemyHealth;
    private int m_enemyDamage;

    [Header("Logic Component")]
    public GameObject enemyObject;
    public Animator animator;
    public BoxCollider2D enemyCollider;
    public EnemyChase enemyChase;
    public PlayerController Player;

    private void Start()
    {
        m_enemyHealth = enemyStatus.Health;
        m_enemyDamage = enemyStatus.Damage;
    }

    private void Update()
    {
        if(m_enemyHealth <= 0)
        {
            animator.SetTrigger("IsDead");
        }
    }

    public void DeathEffect()
    {
        enemyCollider.enabled = false;
        enemyChase.enabled = false;
        Player.playerStatus.EXP += enemyStatus.Exp;
    }

    public void DisActive()
    {
        enemyObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (m_enemyHealth > 0)
            {
                animator.SetTrigger("IsHit");
                m_enemyHealth -= 1;
                Debug.Log(gameObject + " damaged! " + "CurrentHealth= " + m_enemyHealth);
            }
        }

        if (collision.CompareTag("Player"))
        {
            Debug.Log(gameObject + "attacked!");
            Player.playerStatus.HP -= m_enemyDamage;
        }
    }
}
