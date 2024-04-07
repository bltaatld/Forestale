using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Status")]
    public EnemyStatus enemyStatus;
    public int enemyHealth;
    public int enemyDamage;

    [Header("System")]
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;
    public Animator animator;
    public GameObject enemyObject;
    public BoxCollider2D enemyCollider;
    public ColorPulse colorPulse;
    public TriggerTracker triggerTracker;
    
    public float bounceForce = 5f;
    public float currentmoveSpeed;

    public bool isHit;
    public bool isFoundPlayer;

    private PlayerController playerStatus;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerMove = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        enemyHealth = enemyStatus.Health;
        enemyDamage = enemyStatus.Damage;
    }

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            animator.SetTrigger("IsDead");
        }
        if (triggerTracker.triggered)
        {
            isFoundPlayer = true;
        }
        else
        {
            isFoundPlayer = false;
        }
    }

    void FixedUpdate()
    {
        if (!isHit && isFoundPlayer)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (enemyHealth > 0)
            {
                animator.SetTrigger("IsHit");
                enemyHealth -= (int)playerStatus.damagedOutput.NormalDamage;
                colorPulse.Pulse(new Color(100f / 255f, 190f / 255f, 255f / 255f), 0.5f);

                Vector2 direction = (transform.position - player.position).normalized;
                rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);

                Debug.Log(gameObject + " damaged! " + "CurrentHealth = " + enemyHealth);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log(gameObject + "attacked!");
            playerStatus.playerStatus.HP -= enemyDamage;
            playerStatus.PlayerStatusCheck();
        }
    }
    private void OnDisable()
    {
        isFoundPlayer = false;
    }

    public void DisActive()
    {
        enemyObject.SetActive(false);
    }

    public void Respawn()
    {
        enemyHealth = enemyStatus.Health;
        enemyDamage = enemyStatus.Damage;
        enemyCollider.enabled = true;
        enemyObject.SetActive(true);
    }
}
