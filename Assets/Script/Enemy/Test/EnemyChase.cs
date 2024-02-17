using System.Collections;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Enemy Movement")]
    public GameObject targetObject;
    public float moveSpeed = 3f;
    public float chaseSpeedMultiplier = 2f;
    public float randomMoveInterval = 2f;
    public bool isStop;
    public bool isDitected;

    [Header("Enemy Chase Range")]
    public float detectionRadius = 5f;
    public float randomMoveRangeRadius = 7f;

    [Header("Private Value")]
    [SerializeField] private Transform player;
    [SerializeField] private float randomMoveTimer = 0f;
    [SerializeField] private Vector2 randomMovePosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(targetObject.transform.position, player.position) <= detectionRadius && targetObject && isStop == false)
        {
            FollowPlayer();
        }
        else if (isStop == false)
        {
            isDitected = false;
            CircularRandomMovement();
        }
    }

    void DitectAction()
    {
        if (!isDitected)
        {
            Debug.Log("Player detected!");
            isDitected = true;
        }
    }

    void FollowPlayer()
    {
        DitectAction();
        Vector2 direction = (player.position - targetObject.transform.position).normalized;
        float speed = moveSpeed * chaseSpeedMultiplier;
        targetObject.transform.Translate(direction * speed * Time.deltaTime);
    }

    void SetCircularRandomMovePosition()
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(0f, randomMoveRangeRadius);
        randomMovePosition = (Vector2)transform.position + new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * randomRadius;
    }

    void CircularRandomMovement()
    {
        randomMoveTimer -= Time.deltaTime;

        if (randomMoveTimer <= 0f)
        {
            SetCircularRandomMovePosition();
            randomMoveTimer = randomMoveInterval;
        }

        targetObject.transform.position = Vector2.MoveTowards(targetObject.transform.position, randomMovePosition, moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, randomMoveRangeRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetObject.transform.position, detectionRadius);
    }
}
