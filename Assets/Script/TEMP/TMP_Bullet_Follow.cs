using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_Bullet_Follow : MonoBehaviour
{
    public Vector3 vec;

    public Transform playerTransform;
    public float chaseSpeed = 5.0f;
    public GameObject particleFX;

    void Update()
    {
        playerTransform = GameObject.Find("Player").transform;

        transform.Rotate(vec);

        if (playerTransform != null)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            Vector3 moveVector = directionToPlayer.normalized * chaseSpeed * Time.deltaTime;

            transform.Translate(moveVector, Space.World);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Instantiate(particleFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            Instantiate(particleFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
