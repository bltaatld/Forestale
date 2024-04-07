using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    [Header("Logic Component")]
    public GameObject enemyObject;
    public BoxCollider2D enemyCollider;
    public EnemyChase enemyChase;
    public PlayerController playerController;
    public ItemDrop itemDrop;

    [Header("System Value")]
    public bool respawnStop;
    public bool isNeedChase;
    public EnemyStatus enemyStatus;
    public int m_enemyHealth;
    public int m_enemyDamage;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (enemyObject.GetComponent<TE_EnemyChase>())
        {
            enemyStatus = enemyObject.GetComponent<TE_EnemyChase>().enemyStatus;
            m_enemyHealth = enemyStatus.Health;
            m_enemyDamage = enemyStatus.Damage;
        }

        if (enemyObject.GetComponent<ShootEnemy>())
        {
            enemyStatus = enemyObject.GetComponent<ShootEnemy>().enemyStatus;
            m_enemyHealth = enemyStatus.Health;
            m_enemyDamage = enemyStatus.Damage;
        }

        if (enemyObject.GetComponent<ThrowEnemy>())
        {
            enemyStatus = enemyObject.GetComponent<ThrowEnemy>().enemyStatus;
            m_enemyHealth = enemyStatus.Health;
            m_enemyDamage = enemyStatus.Damage;
        }
    }

    public void DeathEffect()
    {
        itemDrop.ItemInstantiate();
        enemyCollider.enabled = false;
        
        if (isNeedChase)
        {
            enemyChase.enabled = false;
        }

        playerController.playerStatus.currentEXP += enemyStatus.Exp;

        if (playerController.playerStatus.WP < playerController.playerMaxWP)
        {
            playerController.playerStatus.WP += enemyStatus.WP;
        }
    }

    public void DisActive()
    {
        enemyObject.SetActive(false);
    }

    public void Respawn()
    {
        if (!respawnStop)
        {
            if (enemyObject.GetComponent<TE_EnemyChase>())
            {
                enemyObject.GetComponent<TE_EnemyChase>().enemyHealth = enemyStatus.Health;
                enemyObject.GetComponent<TE_EnemyChase>().enemyDamage = enemyStatus.Damage;
            }

            if (enemyObject.GetComponent<ShootEnemy>())
            {
                enemyObject.GetComponent<ShootEnemy>().enemyHealth = enemyStatus.Health;
                enemyObject.GetComponent<ShootEnemy>().enemyDamage = enemyStatus.Damage;
            }

            if (enemyObject.GetComponent<ThrowEnemy>())
            {
                enemyObject.GetComponent<ThrowEnemy>().enemyHealth = enemyStatus.Health;
                enemyObject.GetComponent<ThrowEnemy>().enemyDamage = enemyStatus.Damage;
            }

            if (isNeedChase)
            {
                enemyChase.isStop = false;
                enemyChase.enabled = true;
            }

            enemyCollider.enabled = true;
            enemyObject.SetActive(true);
        }
    }
}
