using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSite : MonoBehaviour
{
    public bool isActive;
    public EnemySpawnManager spawnManager;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && isActive)
        {
            spawnManager.RespawnEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = false;
        }
    }
}
