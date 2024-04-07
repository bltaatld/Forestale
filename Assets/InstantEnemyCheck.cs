using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEnemyCheck : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] doors;
    public GameObject[] activeTarget;
    private bool isActiveOnece;

    private void Update()
    {
        bool allInactive = true;

        foreach (GameObject objects in enemy)
        {
            if (objects.activeSelf)
            {
                allInactive = false;
                break;
            }
        }

        if (allInactive)
        {
            for (int j = 0; j < doors.Length; j++)
            {
                doors[j].SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isActiveOnece)
            {
                for (int j = 0; j < doors.Length; j++)
                {
                    doors[j].SetActive(true);
                    activeTarget[j].SetActive(true);
                    isActiveOnece = true;
                }
            }
        }
    }
}
