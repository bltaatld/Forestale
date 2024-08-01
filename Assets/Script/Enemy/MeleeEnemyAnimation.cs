using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAnimation : MonoBehaviour
{
    public Animator animator;
    private Vector3 previousPosition;
    private bool isMoving;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 movementDirection = (transform.position - previousPosition).normalized;
        previousPosition = transform.position;
        isMoving = movementDirection.magnitude > 0.1f;

        if (isMoving)
        {
            SetAnimation(movementDirection);
        }
        else
        {
            animator.SetInteger("WalkX", 0);
            animator.SetInteger("WalkY", 0);
        }
    }

    void SetAnimation(Vector3 direction)
    {
        float x = direction.x;
        float y = direction.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            animator.SetInteger("WalkX", (int)Mathf.Sign(x));
            animator.SetInteger("WalkY", 0);
        }
        else if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            animator.SetInteger("WalkX", 0);
            animator.SetInteger("WalkY", (int)Mathf.Sign(y));
        }
    }
}
