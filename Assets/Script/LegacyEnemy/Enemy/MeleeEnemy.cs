using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;
    public Animator anim;

    [SerializeField] TriggerTracker PlayerTrigger;
    [SerializeField] TriggerTracker AttackTrigger;

    public float bounceForce = 5f;
    public float currentmoveSpeed;
    public float attackCooldown;
    public float AttackTime;

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

        if (PlayerTrigger.triggered)
        {
            AttackTime += Time.deltaTime;

            if (AttackTime >= attackCooldown)
            {
                anim.SetTrigger("IsAttack");
                AttackTime = 0f;
            }
        }

        if (AttackTrigger.triggered)
        {
            if (!isFoundPlayer)
            {
                //PlayerMove.GetComponent<MainCharacter>().Damage(1);
                isFoundPlayer = true;
            }
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

    public void CheckReset()
    {
        isFoundPlayer = false;
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
