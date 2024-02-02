using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSite : MonoBehaviour
{
    public bool isActive;
    public bool isClicked;
    public GameObject statusUIObject;
    public EnemySpawnManager spawnManager;
    public PlayerStatusUI statusUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isActive)
        {
            isClicked = !isClicked;

            if (isClicked)
            {
                statusUIObject.SetActive(false);
                statusUI.healthHeartBar.DrawHearts();
                spawnManager.RespawnEnemy();
            }
            else
            {
                statusUIObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            statusUI.CaculateStatus();
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
