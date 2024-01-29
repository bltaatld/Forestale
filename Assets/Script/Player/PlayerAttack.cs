using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerController Pcontroller;
    public bool isAttack;

    public float attackAfterAttackDelay = 0.1f;
    public float moveAfterAttackDelay = 0.4f;
    private float attackTimer;

    private void Start()
    {
        attackTimer = moveAfterAttackDelay;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X) && attackTimer >= attackAfterAttackDelay)
        {
            isAttack = true;
            Pcontroller.animator.SetTrigger("IsAttack");
            Pcontroller.moveSpeed = 0f;
            attackTimer = 0f;
        }

        else if (attackTimer >= moveAfterAttackDelay)
        {
            isAttack = false;
            if (Pcontroller.moveSpeed == 0f)
            {
                Pcontroller.moveSpeed = Pcontroller.maxMoveSpeed;
            }
        }
        attackTimer += Time.deltaTime;
        attackTimer = Mathf.Min(attackTimer, moveAfterAttackDelay);
    }
}
