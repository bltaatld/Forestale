using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSite : MonoBehaviour
{
    public bool isActive;
    private bool isClicked;
    public GameObject statusUIObject;
    public EnemySpawnManager spawnManager;
    public PlayerStatusUI statusUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isActive)
        {
            // Toggle isClicked when Z is pressed
            isClicked = !isClicked;

            if (isClicked)
            {
                // If isClicked is true, deactivate status UI and respawn enemies
                statusUIObject.SetActive(false);
                statusUI.healthHeartBar.DrawHearts();
                spawnManager.RespawnEnemy();
            }
            else
            {
                // If isClicked is false, activate status UI
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
