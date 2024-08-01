using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    public Transform playerTransform;
    public Dialogue dialogue;
    public Animator animator;
    public Vector3[] waypoints;
    private Vector3 startPosition;
    private int currentWaypointIndex = 0;
    public float speed = 5f;
    public float hInput;
    public float vInput;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if(dialogue.triggerActive)
        {
            animator.SetBool("isWatch", true);
            WatchPlayer();
        }

        if (!dialogue.triggerActive)
        {
            animator.SetBool("isWatch", false);
            Vector3 targetPosition = waypoints[currentWaypointIndex];
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;

                    /* total distance
                    float totalDistance = Vector3.Distance(transform.position, startPosition);
                    Debug.Log("Total Distance Traveled: " + totalDistance);
                    */
                }
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }

            Vector3 movementDirection = (targetPosition - transform.position).normalized;
            hInput = Mathf.Sign(movementDirection.x);
            vInput = Mathf.Sign(movementDirection.y);

            animator.SetInteger("H", (int)hInput);
            animator.SetInteger("V", (int)vInput);
        }

        Debug.DrawLine(startPosition, transform.position, Color.green);
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(waypoints[i], 0.1f);
            if (i > 0)
                Debug.DrawLine(waypoints[i - 1], waypoints[i], Color.blue);
        }
        if (waypoints.Length > 1)
            Debug.DrawLine(waypoints[waypoints.Length - 1], waypoints[0], Color.blue);
    }

    void WatchPlayer()
    {
        Vector3 relativePosition = playerTransform.position - transform.position;

        if (Mathf.Abs(relativePosition.x) > Mathf.Abs(relativePosition.y))
        {
            animator.SetInteger("watchX", (int)Mathf.Sign(relativePosition.x));
            animator.SetInteger("watchY", 0);
        }
        else
        {
            animator.SetInteger("watchY", (int)Mathf.Sign(relativePosition.y));
            animator.SetInteger("watchX", 0);
        }
    }
}
