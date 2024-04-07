using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CutSceneTalk : MonoBehaviour
{
    public Transform targetTransform;
    public TriggerTracker triggerTracker;
    public Dialogue targetDialogue;
    public bool isActive;
    public bool isStartCheck;
    public bool isNeedManager;
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
        if (targetDialogue.isEnded && !isNeedManager)
        {
            playerController.canMove = true;
        }

        if (triggerTracker.triggered)
        {
            isStartCheck = true;
            isActive = true;
        }

        if (currentWaypointIndex < waypoints.Length && isActive)
        {
            MoveToTargetPostion();
        }
        else if (currentWaypointIndex >= waypoints.Length)
        {
            animator.SetBool("isEnd", true);
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
}
