using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<Vector3> mainEnemyPositions = new List<Vector3>();
    public List<GameObject> mainEnemyObject = new List<GameObject>();

    void Start()
    {
        CheckEnemyPosition();
    }

    public void CheckEnemyPosition()
    {
        mainEnemyPositions.Clear();
        mainEnemyObject.Clear();

        // Enemy 태그를 가지는 모든 오브젝트를 찾습니다.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyMain");

        // 각각의 적 오브젝트의 위치 정보를 리스트에 저장합니다.
        foreach (GameObject enemy in enemies)
        {
            mainEnemyObject.Add(enemy);
            mainEnemyPositions.Add(enemy.transform.position);
        }

        foreach (Vector3 position in mainEnemyPositions)
        {
            Debug.Log("Enemy Position: " + position);
        }

    }

    public void RespawnEnemy()
    {
        foreach (GameObject mainEnemy in mainEnemyObject)
        {
            mainEnemy.SetActive(true);
        }

        GameObject[] mainEnemies = GameObject.FindGameObjectsWithTag("EnemyMain");
        Debug.Log(mainEnemies.Length);

        for (int i = 0; i < mainEnemies.Length; i++)
        {
            mainEnemies[i].GetComponent<TE_EnemyChase>().Respawn();
            mainEnemies[i].transform.position = mainEnemyPositions[i];
        }
    }
}
