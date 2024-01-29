using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TE_EnemyChase : MonoBehaviour
{
    [Header("Status")]
    public EnemyStatus enemyStatus;
    public int m_enemyHealth;
    public int m_enemyDamage;

    [Header("Logic Component")]
    public GameObject enemyObject;
    public Animator animator;
    public BoxCollider2D enemyCollider;
    public EnemyChase enemyChase;
    public PlayerController Player;
    public float enemyCoolDown = 2.0f;

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
        Player.playerStatus.currentEXP += enemyStatus.Exp;
    }

    public void DisActive()
    {
        enemyObject.SetActive(false);
    }

    public void Respawn()
    {
        m_enemyHealth = enemyStatus.Health;
        m_enemyDamage = enemyStatus.Damage;
        enemyCollider.enabled = true;
        enemyChase.enabled = true;
        enemyChase.isStop = false;
        enemyObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (m_enemyHealth > 0 && !enemyChase.isStop)
            {
                StartCoroutine(HitCooldown());
                animator.SetTrigger("IsHit");
                m_enemyHealth -= (int)Player.damagedOutput.NormalDamage;
                Debug.Log(gameObject + " damaged! " + "CurrentHealth= " + m_enemyHealth);
            }
        }
    }

    IEnumerator HitCooldown()
    {
        enemyChase.isStop = true;
        yield return new WaitForSeconds(enemyCoolDown);
        enemyChase.isStop = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log(gameObject + "attacked!");
            Player.PlayerStatusCheck();
            Player.playerStatus.HP -= m_enemyDamage;
        }
    }
}
