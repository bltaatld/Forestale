using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMP_Enemy_Stat : MonoBehaviour
{
    public int enemyHP;
    public int enemyMAXHP;
    public Slider healthSlider;

    public GameObject bulletPrefab_arrow;
    public GameObject bulletPrefab_big;
    public GameObject bulletPrefab_small;

    public float spawnRadius = 5f;
    public int bulletCountP2;
    public int bulletCountP3;

    public float intervalMin = 1f;  // 최소 간격 (초)
    public float intervalMax = 3f;  // 최대 간격 (초)
    public int numberOfFunctions = 3;  // 실행할 함수 개수

    public GameObject ClearScene;

    void Start()
    {
        // 일정한 간격으로 랜덤하게 함수를 호출
        InvokeRepeating("InvokeRandomFunctions", 0f, Random.Range(intervalMin, intervalMax));
    }

    private void Update()
    {
        float normalizedHealth = (float)enemyHP / enemyMAXHP;
        healthSlider.value = normalizedHealth;

        if (enemyHP <= 0)
        {
            ClearScene.SetActive(true);
        }
    }

    void InvokeRandomFunctions()
    {
        // 랜덤한 함수를 지정된 횟수만큼 호출
        for (int i = 0; i < numberOfFunctions; i++)
        {
            InvokeRandomFunction();
        }
    }

    void InvokeRandomFunction()
    {
        // 여기에 실행할 랜덤한 함수를 추가
        int randomFunctionIndex = Random.Range(0, 3);  // 예시로 3개의 함수 중 하나를 랜덤하게 선택
        switch (randomFunctionIndex)
        {
            case 0:
                pattern1();
                break;
            case 1:
                pattern2();
                break;
            case 2:
                pattern3();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            enemyHP -= 1;
        }
    }

    public void pattern1()
    {
        Vector3 spawnPosition = new Vector3 (65.11f, 3.64f, 0f);
        Instantiate(bulletPrefab_arrow, spawnPosition, Quaternion.identity);

        Vector3 spawnPosition1 = new Vector3(89.26f, 3.64f, 0f);
        Instantiate(bulletPrefab_arrow, spawnPosition1, Quaternion.identity);

        Vector3 spawnPosition2 = new Vector3(85.22f, 3.64f, 0f);
        Instantiate(bulletPrefab_arrow, spawnPosition2, Quaternion.identity);

        Vector3 spawnPosition3 = new Vector3(81.18f, 3.64f, 0f);
        Instantiate(bulletPrefab_arrow, spawnPosition3, Quaternion.identity);

        Vector3 spawnPosition4 = new Vector3(76.89f, 3.64f, 0f);
        Instantiate(bulletPrefab_arrow, spawnPosition4, Quaternion.identity);

        Vector3 spawnPosition5 = new Vector3(72.85f, 3.64f, 0f);
        Instantiate(bulletPrefab_arrow, spawnPosition5, Quaternion.identity);

        Vector3 spawnPosition6 = new Vector3(68.81f, 3.64f, 0f);
        Instantiate(bulletPrefab_arrow, spawnPosition6, Quaternion.identity);
    }

    public void pattern2()
    {
        for (int i = 0; i < bulletCountP2; i++)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2);
            float randomRadius = Random.Range(0f, spawnRadius);

            // 극좌표를 직교좌표로 변환하여 스폰 위치 계산
            float spawnX = randomRadius * Mathf.Cos(randomAngle);
            float spawnY = randomRadius * Mathf.Sin(randomAngle);

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f) + transform.position;

            // 오브젝트를 스폰 위치에 생성
            Instantiate(bulletPrefab_small, spawnPosition, Quaternion.identity);
        }
    }

    public void pattern3()
    {
        for (int i = 0; i < bulletCountP3; i++)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2);
            float randomRadius = Random.Range(0f, spawnRadius);

            // 극좌표를 직교좌표로 변환하여 스폰 위치 계산
            float spawnX = randomRadius * Mathf.Cos(randomAngle);
            float spawnY = randomRadius * Mathf.Sin(randomAngle);

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f) + transform.position;

            // 오브젝트를 스폰 위치에 생성
            Instantiate(bulletPrefab_big, spawnPosition, Quaternion.identity);
        }
    }
}
