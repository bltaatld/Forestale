using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;

    public GameObject prefabToInstantiate;
    public Animator anim;
    public SpriteRenderer sprite;

    public float bounceForce = 5f;
    public float currentmoveSpeed;
    public float instantiateDelay = 3f;
    private float instantiateTimer = 0f;
    public float spawnRadius = 5f;

    public bool isHit;
    public bool isFoundPlayer;
    public bool isSlow;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerMove = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigid.AddForce(direction * currentmoveSpeed);
        }

        instantiateTimer += Time.deltaTime;

        if (instantiateTimer >= instantiateDelay)
        {
            anim.SetTrigger("IsSummon");
            instantiateTimer = 0f;
        }

        // �÷��̾� ��ġ�� ���� ��������Ʈ�� �¿�� ������
        if (player.position.x < transform.position.x)
        {
            // �÷��̾ ��������� ���ʿ� �ִ� ���
            GetComponent<SpriteRenderer>().flipX = false;
            sprite.flipX = false;
        }
        else
        {
            // �÷��̾ ��������� �����ʿ� �ִ� ���
            GetComponent<SpriteRenderer>().flipX = true;
            sprite.flipX = true;
        }
    }

    public void InstantiatePrefabInRandomPosition()
    {
        // ���� �ݰ� ���� ������ ��ġ ���
        Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0f) + transform.position;

        // ������ ����
        Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = (transform.position - player.position).normalized;
            rigid.AddForce(direction * bounceForce, ForceMode2D.Impulse);
            isHit = true;
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