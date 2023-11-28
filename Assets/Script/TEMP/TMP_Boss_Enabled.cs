using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_Boss_Enabled : MonoBehaviour
{
    public GameObject boss;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            boss.SetActive(true);
        }
    }
}
