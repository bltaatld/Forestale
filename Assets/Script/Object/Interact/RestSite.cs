using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSite : MonoBehaviour
{
    public bool isActive;
    public bool isClicked;
    public bool isNeedRespawn;
    public GameObject statusUIObject;
    public EnemySpawnManager spawnManager;
    public PlayerStatusUI statusUI;

    private void Awake()
    {
        statusUIObject = GameObject.Find("PlayeStatuslUI").transform.GetChild(0).gameObject;
        statusUI = GameObject.Find("PlayeStatuslUI").GetComponent<PlayerStatusUI>();
        spawnManager = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isActive)
        {
            isClicked = !isClicked;

            if (isClicked)
            {
                Time.timeScale = 1f;
                statusUIObject.SetActive(false);
                statusUI.healthHeartBar.DrawHearts();

                if (isNeedRespawn)
                {
                    spawnManager.RespawnEnemy();
                }
            }
            else
            {
                Time.timeScale = 0f;
                statusUIObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            statusUI.CaculateStatus();
            statusUI.healthHeartBar.DrawHearts();
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
