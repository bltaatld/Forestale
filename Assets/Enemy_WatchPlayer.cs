using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WatchPlayer : MonoBehaviour
{
    public Animator anim;
    public Transform targetParent;
    public Transform watchTarget;
    public float detectionRadius = 5f;
    public float attackCoolDown;
    public Color gizmoColor = Color.red;
    public bool isFollow;

    private void OnEnable()
    {
        watchTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.DrawWireSphere(collider.bounds.center, detectionRadius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, watchTarget.position);

        if (distanceToPlayer <= detectionRadius)
        {
            FollowTarget();
        }
        else
        {
            FollowParent();
        }
    }

    void FollowParent()
    {
        Vector2 playerDirection = targetParent.position - transform.position;
        float angleToPlayer = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        if (angleToPlayer >= -45 && angleToPlayer < 45)
        {
            anim.SetInteger("AttackX", 1);
            anim.SetInteger("AttackY", 0);
        }
        else if (angleToPlayer >= 45 && angleToPlayer < 135)
        {
            anim.SetInteger("AttackY", 1);
            anim.SetInteger("AttackX", 0);
        }
        else if (angleToPlayer >= 135 || angleToPlayer < -135)
        {
            anim.SetInteger("AttackX", -1);
            anim.SetInteger("AttackY", 0);
        }
        else if (angleToPlayer >= -135 && angleToPlayer < -45)
        {
            anim.SetInteger("AttackY", -1);
            anim.SetInteger("AttackX", 0);
        }
    }

    void FollowTarget()
    {
        Vector2 playerDirection = watchTarget.position - transform.position;
        float angleToPlayer = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        if (angleToPlayer >= -45 && angleToPlayer < 45)
        {
            anim.SetInteger("AttackX", 1);
            anim.SetInteger("AttackY", 0);
        }
        else if (angleToPlayer >= 45 && angleToPlayer < 135)
        {
            anim.SetInteger("AttackY", 1);
            anim.SetInteger("AttackX", 0);
        }
        else if (angleToPlayer >= 135 || angleToPlayer < -135)
        {
            anim.SetInteger("AttackX", -1);
            anim.SetInteger("AttackY", 0);
        }
        else if (angleToPlayer >= -135 && angleToPlayer < -45)
        {
            anim.SetInteger("AttackY", -1);
            anim.SetInteger("AttackX", 0);
        }
    }
}
