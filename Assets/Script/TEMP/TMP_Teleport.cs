using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_Teleport : MonoBehaviour
{
    public Transform teleportDestination;
    public Canvas uiCanvas;
    public float teleportDelay = 2f;

    private void Start()
    {
        // ���� �ÿ� UI�� ��Ȱ��ȭ
        uiCanvas.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ������ ���� �� ����
        if (other.CompareTag("Player"))
        {
            Teleport(other.gameObject);
        }
    }

    void Teleport(GameObject player)
    {
        // "Player" ������Ʈ�� Ư�� ��ġ�� �ڷ���Ʈ
        player.transform.position = teleportDestination.position;

        // ���� �ð� �Ŀ� UI�� ǥ���ϰ� �����
        ShowUI();
    }

    void ShowUI()
    {
        // UI�� Ȱ��ȭ
        uiCanvas.enabled = true;

        // ���� �ð� �Ŀ� UI�� ��Ȱ��ȭ
        Invoke("HideUI", teleportDelay);
    }

    void HideUI()
    {
        // UI�� ��Ȱ��ȭ
        uiCanvas.enabled = false;
    }
}
