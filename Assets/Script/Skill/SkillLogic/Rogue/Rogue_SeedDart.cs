using System.Collections;
using UnityEngine;

public class Rogue_SeedDart : MonoBehaviour
{
    public Animator animator;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float spreadAngle = 30f;
    public int projectileCount = 3;
    public float shootInterval = 0.1f; // 발사 간격

    private int moveX;
    private int moveY;

    private void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();

        moveX = animator.GetInteger("MoveX");
        moveY = animator.GetInteger("MoveY");

        StartCoroutine(ShootProjectiles());
    }

    IEnumerator ShootProjectiles()
    {
        Vector2 direction = Vector2.zero;

        if (moveX == -1)
            direction = Vector2.left;
        else if (moveX == 1)
            direction = Vector2.right;
        else if (moveY == 1)
            direction = Vector2.up;
        else if (moveY == -1)
            direction = Vector2.down;

        for (int i = 0; i < projectileCount; i++)
        {
            Shoot(direction);
            yield return new WaitForSeconds(shootInterval);
        }

        Destroy(gameObject, 0.5f);
    }

    void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on the projectile prefab.");
        }
    }
}
