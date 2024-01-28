using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerAttack pAttack;
    public float moveSpeed; // 플레이어 이동 속도
    public float rollDuration = 1f;
    public float rollSpeed = 10f;
    public Rigidbody2D rb;
    public Animator animator; // Animator 컴포넌트 참조
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
            // 수평 및 수직 입력 가져오기
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // 이동 방향 설정
            moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;


            if (!pAttack.isAttack)
            {
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
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
