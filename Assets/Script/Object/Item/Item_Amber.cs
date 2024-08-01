using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Amber : MonoBehaviour
{
    private PlayerController player;
    public Animator anim;

    private void Start()
    {
        GameObject foundObject = GameObject.Find("Player");
        player = foundObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound(13);
            player.systemValue.Amber += 1;
            anim.SetTrigger("isActive");
        }
    }
}
