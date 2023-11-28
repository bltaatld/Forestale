using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerAttack pAttack;
    public float moveSpeed; // �÷��̾� �̵� �ӵ�
    public float rollDistance = 3f; // ������ �̵� �Ÿ�
    public float rollDuration = 0.5f; // ������ ���� �ð�
    public Animator animator; // Animator ������Ʈ ����
    public Vector3 moveDirection;

    void FixedUpdate()
    {
        // ���� �� ���� �Է� ��������
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // �̵� ���� ����
        moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;


        if (!pAttack.isAttack)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        // ������ �Է� ����
        if (Input.GetKeyDown(KeyCode.C) && !pAttack.isAttack && moveDirection != Vector3.zero)
        {
            // ������ ����
            StartCoroutine(Roll(moveDirection));
        }

        if (moveDirection != Vector3.zero && !pAttack.isAttack)
        {
            animator.SetBool("IsWalking", true);

            // �� ���⿡ ���� �ٸ� �ִϸ��̼��� ���
            if (moveDirection.x > 0) // ������ �̵�
            {
                animator.SetInteger("MoveX", 1);
                animator.SetInteger("MoveY", 0);
            }
            else if (moveDirection.x < 0) // ���� �̵�
            {
                animator.SetInteger("MoveX", -1);
                animator.SetInteger("MoveY", 0);
            }
            else if (moveDirection.y > 0) // ���� �̵�
            {
                animator.SetInteger("MoveX", 0);
                animator.SetInteger("MoveY", 1);
            }
            else if (moveDirection.y < 0) // �Ʒ��� �̵�
            {
                animator.SetInteger("MoveX", 0);
                animator.SetInteger("MoveY", -1);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false); // �ȴ� �ִϸ��̼� ����
        }
    }

    // ������ �ڷ�ƾ
    public IEnumerator Roll(Vector3 rollDir)
    {
        animator.SetTrigger("IsRoll");

        // ������ �̵�
        Vector3 rollDirection = rollDir; // ���� ���� �ִ� �������� �̵�
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
