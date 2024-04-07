using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBullet : MonoBehaviour
{
    public int damage;
    public float delayInSeconds = 3f;
    public GameObject Effect;
    private PlayerController playerController;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        LookAtPlayerDirection();
        Invoke("DestroyObject", delayInSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(gameObject + "attacked!");
            playerController.playerStatus.HP -= damage;
            playerController.PlayerStatusCheck();

            Instantiate(Effect);
            Destroy(gameObject);
        }
    }

    void LookAtPlayerDirection()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
