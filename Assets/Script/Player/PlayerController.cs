using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public PlayerAttack pAttack;
	public float moveSpeed; // �÷��̾� �̵� �ӵ�
	public float maxMoveSpeed;
	public float rollDuration = 1f;
	public float rollSpeed = 10f;
	public Rigidbody2D rb;
	public Animator animator; // Animator ������Ʈ ����
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
			}
			else
			{
				animator.SetBool("IsWalking", false); // �ȴ� �ִϸ��̼� ����
			}

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
			Debug.LogError("������ 1 �̻��̾�� �մϴ�.");
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
			// ���� ĸ �̻��� ��� ó��
			Debug.LogWarning("�ִ� ������ �ʰ��Ͽ����ϴ�.");
		}
	}
}
