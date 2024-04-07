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
	public GameObject levelUpEffect;
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
		if (!canMove) 
		{
			animator.SetBool("IsWalking", false);
			return;
		}

		if (Input.GetKeyDown(KeyCode.C) && !pAttack.isAttack && moveDirection != Vector3.zero && !m_isRolling) 
		{
			AudioManager.instance.PlaySound(4);
			StartCoroutine(Roll());
		}

		if (playerStatus.HP <= 0)
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

		CheckLevelUp();
	}

	public void PlayerStatusCheck()
	{
		if (playerStatus.HP >= 0)
		{
			OnPlayerDamaged?.Invoke();
            AudioManager.instance.PlaySound(2);
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
		yield return new WaitForSeconds(rollDuration);

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
		Instantiate(levelUpEffect,transform.position, Quaternion.identity);
		CalculateExperienceForLevel(playerStatus.LV);
        skillUI.ResetSP();
		statusUI.ResetLP();
		statusUI.CaculateStatus();
        statusUI.healthHeartBar.DrawHearts();
    }

    public void CamEffect()
    {
        CameraShake.instance.Shakecamera(8f, 0.5f);
    }

    public void CalculateExperienceForLevel(int level)
	{
		if (level <= 0)
		{
			Debug.LogError("level need more than 1");
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
			Debug.LogWarning("max level over");
		}
	}

    public void MoveTo(Vector2 position)
    {
        transform.position = position;
    }
}
