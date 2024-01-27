using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAttack : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public float detectionRadius = 5f;
    public float attackCoolDown;

    void Start()
    {
        InvokeRepeating("UpdateWithInterval", 0f, attackCoolDown);
    }

    void UpdateWithInterval()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            Vector2 playerDirection = player.position - transform.position;
            float angleToPlayer = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            if (angleToPlayer >= -45 && angleToPlayer < 45)
            {
                Debug.Log("right");
                anim.SetInteger("AttackX", 1);
            }
            else if (angleToPlayer >= 45 && angleToPlayer < 135)
            {
                Debug.Log("up");
                anim.SetInteger("AttackY", 1);
            }
            else if (angleToPlayer >= 135 || angleToPlayer < -135)
            {
                Debug.Log("left");
                anim.SetInteger("AttackX", -1);
            }
            else if (angleToPlayer >= -135 && angleToPlayer < -45)
            {
                Debug.Log("down");
                anim.SetInteger("AttackY", -1);
            }
        }
    }

    public void ResetAttack()
    {
        anim.SetInteger("AttackY", 0);
        anim.SetInteger("AttackX", 0);
    }
}
