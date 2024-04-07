using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue_Dart : MonoBehaviour
{
    public Animator animator;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float spreadAngle = 30f;
    public int projectileCount = 3;

    public int moveX;
    public int moveY;

    private void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();

        moveX = animator.GetInteger("MoveX");
        moveY = animator.GetInteger("MoveY");

        if (moveX == -1)
        {
            Shoot(Vector2.left);
        }
        else if (moveX == 1)
        {
            Shoot(Vector2.right);
        }
        if (moveY == 1)
        {
            Shoot(Vector2.up);
        }
        else if (moveY == -1)
        {
            Shoot(Vector2.down);
        }

        Destroy(gameObject, 0.5f);
    }

    void Shoot(Vector2 direction)
    {
        float startAngle = - spreadAngle / 2f;
        float angleIncrement = spreadAngle / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + (angleIncrement * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector2 shootDirection = rotation * direction;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = shootDirection * projectileSpeed;
        }
    }
}
