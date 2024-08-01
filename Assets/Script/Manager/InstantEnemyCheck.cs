using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEnemyCheck : MonoBehaviour
{
    public SpriteRenderer doorSprite;
    public GameObject[] enemy;
    public GameObject[] doors;
    public GameObject[] activeTarget;
    private bool isActiveOnece;
    private bool isPlay;

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
            if (!isPlay)
            {
                if(doorSprite != null)
                {
                    doorSprite.sprite = null;
                }

                AudioManager.instance.PlaySound(15);
                isPlay = true;
            }

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
                    if(activeTarget.Length == doors.Length)
                    {
                        if (activeTarget[j] != null)
                        {
                            activeTarget[j].SetActive(true);
                        }
                    }
                    doors[j].SetActive(true);
                    isActiveOnece = true;
                }
            }
        }
    }
}
