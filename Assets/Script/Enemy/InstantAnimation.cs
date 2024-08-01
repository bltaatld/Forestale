using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAnimation : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject targetActive;
    public GameObject targetUI;
    public PlayerController playerController;
    public BoxCollider2D playerCollider;

    public void SpriteActive()
    {
        sprite.enabled = true;
        targetActive.SetActive(false);
    }

    public void SpriteDisactive()
    {
        sprite.enabled = false;
    }

    public void ActiveTarget()
    {
        targetActive.SetActive(true);
    }

    public void DeathEffectActive()
    {
        playerController.PlayerDeadCheck();
    }

    public void ActiveCollider() 
    {
        playerController.canMove = true;
        targetUI.SetActive(false);
        playerCollider.enabled = true;
    }

    public void DisactiveCollider()
    {
        playerController.canMove = false;
        targetUI.SetActive(true);
        playerCollider.enabled = false;
    }
}
