using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemProbability
{
    public GameObject item;
    [Range(0, 100)]
    public int probability;
}

public class ItemDrop : MonoBehaviour
{
    public List<ItemProbability> itemProbabilities;

    public void ItemInstantiate()
    {
        int totalProbability = 0;

        foreach (var itemProbability in itemProbabilities)
        {
            totalProbability += itemProbability.probability;
        }

        int randomValue = Random.Range(0, totalProbability);
        int cumulativeProbability = 0;

        foreach (var itemProbability in itemProbabilities)
        {
            cumulativeProbability += itemProbability.probability;
            if (randomValue < cumulativeProbability)
            {
                GameObject newItem = Instantiate(itemProbability.item);
                newItem.transform.position = transform.position;
                return;
            }
        }
    }
}

