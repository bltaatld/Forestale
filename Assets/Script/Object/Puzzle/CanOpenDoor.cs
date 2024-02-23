using System.Collections;
using UnityEngine;

public class CanOpenDoor : MonoBehaviour
{
    public GameObject[] Doors;  // Door 배열로 수정
    public SpriteRenderer sprite;
    public Sprite openSprite;
    public Sprite closeSprite;
    public float moveDistance = 10f; // 이동할 거리
    public float smoothSpeed = 2f;
    public bool isOpen = false;
    public bool canOpen = false;
    private bool isMoving = false;

    private Coroutine moveDoorCoroutine;

    private void Start()
    {
        // 문의 초기 위치를 설정
        if (isOpen)
        {
            MoveDoors(moveDistance); // 이동할 거리만큼 열린 상태로 설정
        }
    }

    private void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isMoving)
            {
                ToggleDoor();
            }
        }
        /*
        if (!canOpen)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isMoving)
            {
                if (isOpen)
                {
                    CloseDoor();
                }

            }
        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Active");
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Disactive");
            canOpen = false;
        }
    }

    private void ToggleDoor()
    {
        Debug.Log("asd");
        if (isOpen && !isMoving)
        {
            CloseDoor();
        }
        else if (!isOpen && !isMoving)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (moveDoorCoroutine != null)
        {
            StopCoroutine(moveDoorCoroutine);
        }
        moveDoorCoroutine = StartCoroutine(MoveDoors(moveDistance));
        isOpen = true;
    }

    private void CloseDoor()
    {
        if (moveDoorCoroutine != null)
        {
            StopCoroutine(moveDoorCoroutine);
        }
        moveDoorCoroutine = StartCoroutine(MoveDoors(-moveDistance));
        isOpen = false;
    }

    private IEnumerator MoveDoors(float distance)
    {
        isMoving = true;

        Vector3[] targetPositions = new Vector3[Doors.Length];

        for (int i = 0; i < Doors.Length; i++)
        {
            // 만약 rotation z가 -90이면 y로 이동
            if (Doors[i].transform.eulerAngles.z == 270)
            {
                targetPositions[i] = Doors[i].transform.position + new Vector3(0f, distance, 0f);
            }
            else
            {
                targetPositions[i] = Doors[i].transform.position + new Vector3(distance, 0f, 0f);
            }
        }

        while (Vector2.Distance(Doors[0].transform.position, targetPositions[0]) > 0.01f)
        {
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].transform.position = Vector3.Lerp(Doors[i].transform.position, targetPositions[i], smoothSpeed * Time.deltaTime);
            }

            yield return null;
        }

        isMoving = false;
    }
}
