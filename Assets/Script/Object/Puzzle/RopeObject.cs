using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RopeObject : MonoBehaviour
{
    public PlayerController playerController;
    public LayerMask pickUpMask;
    public Vector3 currnetDirection;
    public Vector3 savedDirection;
    private float epsilon = 0.0001f;
    public bool isHold;
    public Collider2D ropeMain;
    public GameObject LinePrefab;
    public GameObject Line;
    public float canMoveDistance;

    void DrawLine(LineRenderer lineRenderer,Vector3 start, Vector3 end)
    {
        // 선 그리기
        lineRenderer.positionCount = 2; // 선의 점 개수 (시작과 끝)
        lineRenderer.SetPosition(0, start); // 시작점 설정
        lineRenderer.SetPosition(1, end); // 끝점 설정
    }

    private void Update()
    {
        if (isHold)
        {
            if (ropeMain != null && Line != null)
            {
                DrawLine(Line.GetComponent<LineRenderer>(),ropeMain.transform.position, gameObject.transform.position);
            }

            float distance = Vector2.Distance(ropeMain.transform.position, gameObject.transform.position);
            if (distance > canMoveDistance)
            {
                isHold = false;
                playerController.moveSpeed = 6f;
                Destroy(Line);
            }
        }

        if (playerController.moveDirection != Vector3.zero && (Mathf.Abs(playerController.moveDirection.x - Mathf.Round(playerController.moveDirection.x)) < epsilon))
        {
            currnetDirection = playerController.moveDirection;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + currnetDirection, 0.2f, pickUpMask);
            
            if(ropeMain == null)
            {
                ropeMain = pickUpItem;
            }

            if (pickUpItem)
            {
                Line = Instantiate(LinePrefab);
                playerController.moveSpeed = 1f;
                isHold = true;
            }
        }


        if (Input.GetKeyUp(KeyCode.Z))
        {
            playerController.moveSpeed = 6f;
            isHold = false;
            Destroy(Line);
        }

    }
}
