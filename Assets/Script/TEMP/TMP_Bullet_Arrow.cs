using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMP_Bullet_Arrow : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Transform arrowTransform;
    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void Update()
    {
        direction = new Vector3(0, -1, arrowTransform.position.z).normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
