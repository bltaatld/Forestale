using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeed : MonoBehaviour
{
    public GameObject targetParent;
    public List<GameObject> currentSeed = new List<GameObject>();
    public List<GameObject> seedInventory = new List<GameObject>();

    public void ActiveCurrentSeed()
    {
        foreach (Transform child in targetParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentSeed.Count; i++)
        {
            if (currentSeed[i] != null)
            {
                GameObject newItem = Instantiate(currentSeed[i]);
                newItem.transform.parent = targetParent.transform;
            }
            else
            {
                Debug.LogWarning("currentSeed[" + i + "] is null. Skipping instantiation.");
            }
        }
    }

    public void ResetCurrentSeed()
    {
        foreach (Transform child in targetParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Seed"))
        {
            AddToInventory(other.gameObject);
        }
    }

    private void AddToInventory(GameObject seed)
    {
        seedInventory.Add(seed);
        seed.SetActive(false);
        Debug.Log(seed.name + " added to Seed!");
    }
}
