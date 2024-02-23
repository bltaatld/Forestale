using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public Transform player;
    public GameObject PlayerMove;
    public Rigidbody2D rigid;

    [SerializeField] TriggerTracker Playertrigger;
    
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
        isFoundPlayer = false;
    }
}
