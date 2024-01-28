using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerAttack pAttack;
    public float moveSpeed; // �÷��̾� �̵� �ӵ�
    public float rollDuration = 1f;
    public float rollSpeed = 10f;
    public Rigidbody2D rb;
    public Animator animator; // Animator ������Ʈ ����
    public Vector3 moveDirection;
    public bool canMove;
    [SerializeField] private bool m_isRolling;

    public PlayerStatus playerStatus;
    public ExtraStatus extraPlayerStatus;

    public static event Action OnPlayerDamaged;

    public int playerMaxHP;

    private void Start()
    {
        playerStatus.HP = playerMaxHP;
    }

    private void Update()
    {
        if (!canMove) {  return; }
    
        if (Input.GetKeyDown(KeyCode.C) && !pAttack.isAttack && moveDirection != Vector3.zero && !m_isRolling)
        {
            StartCoroutine(Roll());
        }
    }

    public void PlayerStatusCheck()
    {
        if (playerMaxHP != playerStatus.HP)
        {
            OnPlayerDamaged?.Invoke();
            animator.SetTrigger("IsDamaged");
        }
    }

    void FixedUpdate()
    {
        if (canMove)
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
    }

    public IEnumerator Roll()
    {
        m_isRolling = true;

        Debug.Log("Rolled");
        rb.velocity = new Vector2(moveDirection.x * rollSpeed, moveDirection.y * rollSpeed);
        animator.SetTrigger("IsRoll");
        yield return new WaitForSeconds(rollDuration);

        m_isRolling = false;
    }
}
