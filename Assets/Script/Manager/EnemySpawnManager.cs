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

        // Enemy �±׸� ������ ��� ������Ʈ�� ã���ϴ�.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyMain");

        // ������ �� ������Ʈ�� ��ġ ������ ����Ʈ�� �����մϴ�.
        foreach (GameObject enemy in enemies)
        {
            mainEnemyObject.Add(enemy);
            mainEnemyPositions.Add(enemy.transform.position);
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
