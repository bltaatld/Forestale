using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableNPC : MonoBehaviour
{
    public GameObject dialogue;
    public bool isTalkOtherLine;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.SetActive(false);
        }
    }
}
