using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TE_EnemyChase : MonoBehaviour
{
    [Header("Status")]
    public EnemyStatus enemyStatus;
    public int enemyHealth;
    public int enemyDamage;

    [Header("Logic Component")]
    public GameObject enemyObject;
    public Animator animator;
    public BoxCollider2D enemyCollider;
    public EnemyChase enemyChase;
    public PlayerController playerController;
    public Rigidbody2D rigid;
    public ItemDrop itemDrop;
    public float enemyCoolDown = 2.0f;
    public float bounceForce = 2.0f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyHealth = enemyStatus.Health;
        enemyDamage = enemyStatus.Damage;
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            animator.SetTrigger("IsDead");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (enemyHealth > 0 && !enemyChase.isStop)
            {
                AudioManager.instance.PlaySound(7);
                StartCoroutine(HitCooldown());
                animator.SetTrigger("IsHit");
                enemyHealth -= (int)playerController.damagedOutput.NormalDamage;

                Vector2 direction = (transform.position - player.position).normalized;
                rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
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
            if (!enemyChase.isStop && playerController.playerStatus.HP > 0)
            {
                StartCoroutine(HitCooldown());
                playerController.playerStatus.HP -= enemyDamage;
                playerController.PlayerStatusCheck();

                Vector2 direction = (transform.position - player.position).normalized;
                rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
