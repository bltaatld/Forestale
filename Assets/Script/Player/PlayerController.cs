using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerAttack pAttack;
    public float moveSpeed; // 플레이어 이동 속도
    public float rollDistance = 3f; // 구르기 이동 거리
    public float rollDuration = 0.5f; // 구르기 지속 시간
    public Animator animator; // Animator 컴포넌트 참조
    public Vector3 moveDirection;

    void FixedUpdate()
    {
        // 수평 및 수직 입력 가져오기
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 이동 방향 설정
        moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;


        if (!pAttack.isAttack)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        // 구르기 입력 감지
        if (Input.GetKeyDown(KeyCode.C) && !pAttack.isAttack && moveDirection != Vector3.zero)
        {
            // 구르기 시작
            StartCoroutine(Roll(moveDirection));
        }

        if (moveDirection != Vector3.zero && !pAttack.isAttack)
        {
            animator.SetBool("IsWalking", true);

            // 각 방향에 따라 다른 애니메이션을 재생
            if (moveDirection.x > 0) // 오른쪽 이동
            {
                animator.SetInteger("MoveX", 1);
                animator.SetInteger("MoveY", 0);
            }
            else if (moveDirection.x < 0) // 왼쪽 이동
            {
                animator.SetInteger("MoveX", -1);
                animator.SetInteger("MoveY", 0);
            }
            else if (moveDirection.y > 0) // 위쪽 이동
            {
                animator.SetInteger("MoveX", 0);
                animator.SetInteger("MoveY", 1);
            }
            else if (moveDirection.y < 0) // 아래쪽 이동
            {
                animator.SetInteger("MoveX", 0);
                animator.SetInteger("MoveY", -1);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false); // 걷는 애니메이션 정지
        }
    }

    // 구르기 코루틴
    public IEnumerator Roll(Vector3 rollDir)
    {
        animator.SetTrigger("IsRoll");

        // 구르기 이동
        Vector3 rollDirection = rollDir; // 현재 보고 있는 방향으로 이동
        Vector3 targetPosition = transform.position + rollDirection * rollDistance;
        float elapsedTime = 0f;

        while (elapsedTime < rollDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / rollDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
