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

        // ������� ���� ��������Ʈ ����
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

        // Ư�� ��ư�� ������ �� (��: �����̽���)
        if (Input.GetKeyDown(KeyCode.V))
        {
            buttonPressed = true;
            StartCoroutine(DebugMessageEverySecond());
        }

        // Ư�� ��ư�� �������� ��
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
