using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomItemDrop : MonoBehaviour
{
    public GameObject[] items;
    public float[] itemProbabilities;

    void SpawnRandomItem()
    {
        if (items.Length != itemProbabilities.Length)
        {
            Debug.LogError("Probabilities and items need to get same length");
            return;
        }
            
        float targetValue = Random.Range(0f, 100f);
        float closestValue = FindClosestValue(targetValue, itemProbabilities);

        Debug.Log(targetValue);
        Debug.Log(closestValue);

        for (int i = 0; i < itemProbabilities.Length; i++)
        {
            if (itemProbabilities[i] == closestValue)
            {
                Instantiate(items[i], transform.position, Quaternion.identity);
                Debug.Log(items[i] + "Spawned");
            }
        }

        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnRandomItem();
        }
    }

    float FindClosestValue(float target, float[] valueArray)
    {
        if (valueArray == null || valueArray.Length == 0)
        {
            Debug.LogError("Values array is null or empty.");
            return float.NaN;
        }

        float closestValue = valueArray[0];
        float minDifference = Mathf.Abs(target - closestValue);

        for (int i = 1; i < valueArray.Length; i++)
        {
            float currentDifference = Mathf.Abs(target - valueArray[i]);

            if (currentDifference < minDifference)
            {
                minDifference = currentDifference;
                closestValue = valueArray[i];
            }
        }

        return closestValue;
    }
}
