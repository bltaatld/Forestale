using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReAnimateEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public GameObject prefabToInstantiate;
    public SpriteRenderer sprite;
    public Rigidbody2D rigid;

    public float bounceForce = 5f;
    public float currentmoveSpeed;

    public bool isHit;
    public bool isFoundPlayer;

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


        // �÷��̾� ��ġ�� ���� ��������Ʈ�� �¿�� ������
        if (player.position.x < transform.position.x)
        {
            sprite.flipX = false; 
            // �÷��̾ ��������� ���ʿ� �ִ� ���
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            sprite.flipX = true;
            // �÷��̾ ��������� �����ʿ� �ִ� ���
            GetComponent<SpriteRenderer>().flipX = true;
        }

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

    private void OnDisable()
    {
        if (prefabToInstantiate != null)
        {
            Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        }
    }

    public void RespawnSoundPlay()
    {
        //AudioManager.instance.PlaySound(5);
    }
}
