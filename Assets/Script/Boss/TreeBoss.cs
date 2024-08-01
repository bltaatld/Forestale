using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : MonoBehaviour
{
    [Header("Random Pattern Time")]
    public int minValue = 0;
    public int maxValue = 3;
    public float interval = 5.0f;
    public bool isOnField;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public Vector2 moveAreaMin;
    public Vector2 moveAreaMax;

    [Header("Reference")]
    public Animator animator;
    public Animator coreAnimator;
    public GameObject objectPrefab;
    public GameObject moveObjectPrefab;

    void Start()
    {
        InvokeRepeating("GenerateRandomValue", 0, interval);
        InvokeRepeating("MoveObject", 0, 2.5f);
    }

    void GenerateRandomValue()
    {
        int randomValue = Random.Range(minValue, maxValue);
        animator.SetInteger("CurrentPattern", randomValue);
    }

    public void CamEffect()
    {
        CameraShake.instance.Shakecamera(8f, 0.5f);
    }

    public void SpawnObject()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }

    public void MoveObject()
    {
        coreAnimator.SetTrigger("isMove");
        float randomX = Random.Range(moveAreaMin.x, moveAreaMax.x);
        float randomY = Random.Range(moveAreaMin.y, moveAreaMax.y);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        moveObjectPrefab.transform.position = spawnPosition;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 topLeft = new Vector3(spawnAreaMin.x, spawnAreaMax.y, 0);
        Vector3 topRight = new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0);
        Vector3 bottomLeft = new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0);
        Vector3 bottomRight = new Vector3(spawnAreaMax.x, spawnAreaMin.y, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    public void LaserSound()
    {
        AudioManager.instance.PlaySound(9);
    }

    public void PunchSound()
    {
        AudioManager.instance.PlaySound(10);
    }
}
