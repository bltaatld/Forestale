using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [Header("Status")]
    public EnemyStatus enemyStatus;
    public int enemyHealth;
    public int enemyDamage;

    [Header("System")]
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;
    public Animator anim;
    public ColorPulse colorPulse;
    public string targetLayerName = "EnemyOwl";

    public float bounceForce = 5f;
    public float currentmoveSpeed;

    public GameObject projectilePrefab;
    public int projectileCount = 5;
    public float projectileSpeed = 10f;
    public float spreadAngle = 30f;
    public float shootTime;
    public float ShootCoolDown;

    public bool isHit;
    public bool isMove;
    public bool isFoundPlayer;
    public bool isBark;

    public float rangeRadius = 5f;
    private bool playerInRange = false;

    [Header("Logic Component")]
    public GameObject enemyObject;
    public ItemDrop itemDrop;
    public BoxCollider2D enemyCollider;
    public TriggerTracker triggerTracker;

    private PlayerController playerStatus;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerMove = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        enemyHealth = enemyStatus.Health;
        enemyDamage = enemyStatus.Damage;
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool playerCurrentlyInRange = distance <= rangeRadius;

        if (!playerInRange && playerCurrentlyInRange)
        {
            Debug.Log("Asdasd");
            isMove = true;
        }
        else if (playerInRange && !playerCurrentlyInRange)
        {
            isMove = false;
        }

        playerInRange = playerCurrentlyInRange;


        if (isMove)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }

        shootTime += Time.deltaTime;

        if (shootTime >= ShootCoolDown && isMove)
        {
            anim.SetTrigger("IsShoot");
            FireProjectiles();
            shootTime = 0f;
        }

        if (player.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (enemyHealth <= 0)
        {
            anim.SetTrigger("IsDead");
        }
    }

    public void FireProjectiles()
    {
        isMove = true;
        Vector2 playerDirection = (player.position - transform.position).normalized;
        float startAngle = -spreadAngle / 2f;
        float angleIncrement = spreadAngle / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + (angleIncrement * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector2 direction = rotation * playerDirection;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = direction * projectileSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }


    public void Respawn()
    {
        enemyHealth = enemyStatus.Health;
        enemyDamage = enemyStatus.Damage;
        enemyCollider.enabled = true;
        enemyObject.SetActive(true);
    }

    public void DeathEffect()
    {
        itemDrop.ItemInstantiate();
        enemyCollider.enabled = false;
        playerStatus.playerStatus.currentEXP += enemyStatus.Exp;

        if (playerStatus.playerStatus.WP < playerStatus.playerMaxWP)
        {
            playerStatus.playerStatus.WP += enemyStatus.WP;
        }
    }

    public void DisActive()
    {
        enemyObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (enemyHealth > 0)
            {
                anim.SetTrigger("IsHit");
                enemyHealth -= (int)playerStatus.damagedOutput.NormalDamage;
                colorPulse.Pulse(new Color(1f, 0f, 0f), 0.5f);

                Vector2 direction = (transform.position - player.position).normalized;
                rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);

                Debug.Log(gameObject + " damaged! " + "CurrentHealth = " + enemyHealth);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = (transform.position - player.position).normalized;
            rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
            isHit = true;

            Debug.Log(gameObject + "attacked!");
            playerStatus.playerStatus.HP -= enemyDamage;
            playerStatus.PlayerStatusCheck();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHit = false;
        }
    }
}
