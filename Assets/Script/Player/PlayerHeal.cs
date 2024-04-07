using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour
{
    public Image progressBar;
    public Sprite sprite0Percent;
    public Sprite sprite20Percent;
    public Sprite sprite50Percent;
    public Sprite sprite100Percent;

    public Animator animator;
    public PlayerController playerController;
    public PlayerStatusUI statusUI;

    private bool buttonPressed = false;

    void Update()
    {
        float percentage = playerController.playerStatus.WP / playerController.playerMaxWP;

        // 백분율에 따라 스프라이트 변경
        if (percentage <= 0.2f)
        {
            animator.SetInteger("Percent", 0);
            //progressBar.sprite = sprite0Percent;
        }
        else if (percentage <= 0.5f)
        {
            animator.SetInteger("Percent", 1);
            //progressBar.sprite = sprite20Percent;
        }
        else if (percentage < 1f)
        {
            animator.SetInteger("Percent", 2);
            //progressBar.sprite = sprite50Percent;
        }
        else
        {
            animator.SetInteger("Percent", 3);
            //progressBar.sprite = sprite100Percent;
        }

        // 특정 버튼이 눌렸을 때 (예: 스페이스바)
        if (Input.GetKeyDown(KeyCode.V))
        {
            buttonPressed = true;
            StartCoroutine(DebugMessageEverySecond());
        }

        // 특정 버튼이 떼어졌을 때
        if (Input.GetKeyUp(KeyCode.V))
        {
            buttonPressed = false;
            StopAllCoroutines();
        }
    }

    IEnumerator DebugMessageEverySecond()
    {
        while (buttonPressed)
        {
            yield return new WaitForSeconds(1f);
            if (playerController.playerMaxHP > playerController.playerStatus.HP && playerController.playerStatus.WP > 0)
            {
                playerController.playerStatus.HP += 1f;
                playerController.playerStatus.WP -= 50;
                statusUI.healthHeartBar.DrawHearts();
                Debug.Log("Heal!");
            }
            Debug.Log("1 second passed!");
        }
    }
}
