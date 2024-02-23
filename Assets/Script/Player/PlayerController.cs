using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public PlayerAttack pAttack;
	public float moveSpeed; // 플레이어 이동 속도
	public float maxMoveSpeed;
	public float rollDuration = 1f;
	public float rollSpeed = 10f;
	public Rigidbody2D rb;
	public Animator animator; // Animator 컴포넌트 참조
	public Vector3 moveDirection;
	public bool canMove;
	[SerializeField] private bool m_isRolling;

	public PlayerStatus playerStatus;
	public ExtraStatus extraPlayerStatus;
	public DamagedOutput damagedOutput;
	public SystemValue systemValue;
	public SkillUI skillUI;
	public PlayerStatusUI statusUI;

	public static event Action OnPlayerDamaged;

	public float playerMaxHP;
	public float playerMaxMP;
	public float playerMaxWP;
	public int playerMaxSeed;

	private void Start()
	{
		playerMaxSeed = systemValue.Seed;
		maxMoveSpeed = moveSpeed;
	}

	private void Update()
	{
		if (!canMove) {  return; }
	
		if (Input.GetKeyDown(KeyCode.C) && !pAttack.isAttack && moveDirection != Vector3.zero && !m_isRolling)
		{
			StartCoroutine(Roll());
		}

		CheckLevelUp();
	}

	public void PlayerStatusCheck()
	{
		if (playerStatus.HP >= 0)
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
			}
			else
			{
				animator.SetBool("IsWalking", false); // 걷는 애니메이션 정지
			}

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
	}

	public IEnumerator Roll()
	{
		m_isRolling = true;

		Debug.Log("Rolled");
		rb.velocity = new Vector2(moveDirection.x * rollSpeed, moveDirection.y * rollSpeed);
		animator.SetTrigger("IsRoll");
		gameObject.tag = "PlayerExtra";
		yield return new WaitForSeconds(rollDuration);
		gameObject.tag = "Player";

		m_isRolling = false;
	}

	private void CheckLevelUp()
	{
		if (playerStatus.currentEXP >= playerStatus.needEXP)
		{
			LevelUp();
			playerStatus.currentEXP = 0;
		}
	}

	private void LevelUp()
	{
		playerStatus.LV++;
		CalculateExperienceForLevel(playerStatus.LV);
		skillUI.ResetSP();
		statusUI.ResetLP();
	}


	public void CalculateExperienceForLevel(int level)
	{
		if (level <= 0)
		{
			Debug.LogError("레벨은 1 이상이어야 합니다.");
		}

		if (level <= 10)
		{
			playerStatus.needEXP = (int)15f + (level + Mathf.CeilToInt(level * 20f));
		}
		else if (level <= 20)
		{
			playerStatus.needEXP = (int)225f + (level + Mathf.CeilToInt(level * 10.5f));
		}
		else if (level <= 50)
		{
			playerStatus.needEXP = (int)445f + (level + Mathf.CeilToInt(level * 8.5f));
		}

		else if (level <= 75)
		{
			playerStatus.needEXP = (int)920f + (level + Mathf.CeilToInt(level * 5f));
		}
		else if (level <= 100)
		{
			playerStatus.needEXP = (int)1370f + (level + Mathf.CeilToInt(level * 25f));
		}
		else
		{
			// 레벨 캡 이상의 경우 처리
			Debug.LogWarning("최대 레벨을 초과하였습니다.");
		}
	}
}
