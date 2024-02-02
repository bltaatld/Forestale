using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushObject : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D bushCollider;
    public int Health;
    public ItemDrop itemDrop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            Health -= 1;
            if (Health <= 0)
            {
                itemDrop.ItemInstantiate();
                animator.SetTrigger("IsDead");
                bushCollider.enabled = false;
            }
        }
    }
}
