using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEnemy : MonoBehaviour
{
    [Header("Status")]
    public EnemyStatus enemyStatus;
    public int enemyHealth;
    public int enemyDamage;

    [Header("System")]
    public GameObject prefabToInstantiate;
    public Rigidbody2D rigid;
    public ColorPulse colorPulse;
    public Animator animator;
    public TriggerTracker triggerTracker;

    [Header("SpawnValue")]
    public float bounceForce = 5f;
    public float instantiateDelay = 3f;
    public float spawnRadius = 5f;

    private float instantiateTimer = 0f;
    private bool isFoundPlayer;
    private PlayerController playerStatus;
    private GameObject PlayerMove;
    private Transform player;

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

        if(triggerTracker.triggered)
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
        instantiateTimer += Time.deltaTime;

        if (instantiateTimer >= instantiateDelay && isFoundPlayer)
        {
            InstantiatePrefabInRandomPosition();
            instantiateTimer = 0f;
        }
    }

    private void InstantiatePrefabInRandomPosition()
    {
        Instantiate(prefabToInstantiate, PlayerMove.transform.position, Quaternion.identity);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (enemyHealth > 0)
            {
                animator.SetTrigger("IsHit");
                enemyHealth -= (int)playerStatus.damagedOutput.NormalDamage;
                colorPulse.Pulse(new Color(1f, 0f, 0f), 0.5f);

                Vector2 direction = (transform.position - player.position).normalized;
                rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
            }
        }
    }


    private void OnDisable()
    {
        isFoundPlayer = false;
    }
}
