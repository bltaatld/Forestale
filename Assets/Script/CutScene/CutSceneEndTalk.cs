using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEndTalk : MonoBehaviour
{
    public GameObject DisapperTarget;
    public GameObject activeTarget;
    public GameObject activeTrigger;
    public GameObject shakeTarget;
    public Transform targetTransform;
    public TriggerTracker triggerTracker;
    public Dialogue targetDialogue;
    public bool isEndCheck;
    public bool isActive;
    public bool isNotNeedReset;
    public Vector3[] waypoints;
    public float speed = 5f;

    public Animator animator;
    public float hInput;
    public float vInput;

    private PlayerController playerController;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (triggerTracker.triggered && targetDialogue.isEnded)
        {
            isEndCheck = true;
            isActive = true;
        }

        if (currentWaypointIndex < waypoints.Length && isActive)
        {
            animator.SetBool("isEndMoveStart", true);
            MoveToTargetPostion();
        }

        else if (currentWaypointIndex >= waypoints.Length)
        {
            animator.SetBool("isEndAction", true);

            if (!isNotNeedReset)
            {
                playerController.canMove = true;
                activeTarget.SetActive(true);

                if(activeTrigger != null)
                {
                    activeTrigger.SetActive(true);
                }
                if (shakeTarget != null)
                {
                    HideShake();
                }
                DisapperTarget.SetActive(false);
            }
        }
    }

    public void MoveToTargetPostion()
    {
        playerController.canMove = false;
        Vector3 targetPosition = waypoints[currentWaypointIndex];
        float distanceToTarget = Vector3.Distance(targetTransform.position, targetPosition);

        if (distanceToTarget < 0.1f)
        {
            currentWaypointIndex++;
        }
        else
        {
            targetTransform.position = Vector3.MoveTowards(targetTransform.position, targetPosition, speed * Time.deltaTime);
        }

        Vector3 movementDirection = (targetPosition - transform.position).normalized;
        hInput = Mathf.Sign(movementDirection.x);
        vInput = Mathf.Sign(movementDirection.y);

        animator.SetInteger("H", (int)hInput);
        animator.SetInteger("V", (int)vInput);
    }

    public void ActiveShake()
    {
        shakeTarget.SetActive(true);
    }

    public void HideShake()
    {
        shakeTarget.SetActive(false);
    }
}
