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
        // 시작 시에 UI를 비활성화
        uiCanvas.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있을 때 실행
        if (other.CompareTag("Player"))
        {
            Teleport(other.gameObject);
        }
    }

    void Teleport(GameObject player)
    {
        // "Player" 오브젝트를 특정 위치로 텔레포트
        player.transform.position = teleportDestination.position;

        // 일정 시간 후에 UI를 표시하고 숨기기
        ShowUI();
    }

    void ShowUI()
    {
        // UI를 활성화
        uiCanvas.enabled = true;

        // 일정 시간 후에 UI를 비활성화
        Invoke("HideUI", teleportDelay);
    }

    void HideUI()
    {
        // UI를 비활성화
        uiCanvas.enabled = false;
    }
}
