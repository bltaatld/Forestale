using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject Mention;
    public GameObject targetParent;
    public GameObject[] currentItem;
    public List<GameObject> inventory = new List<GameObject>();

    public void ActiveCurrentItem()
    {
        foreach (Transform child in targetParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentItem.Length; i++)
        {
            if (currentItem[i] != null)
            {
                GameObject newItem = Instantiate(currentItem[i]);
                newItem.transform.parent = targetParent.transform;
            }
            else
            {
                Debug.LogWarning("currentItem[" + i + "] is null. Skipping instantiation.");
            }
        }
    }

    public void ResetCurrentItem()
    {
        foreach (Transform child in targetParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            AddToInventory(other.gameObject);
        }
    }

    private void AddToInventory(GameObject item)
    {
        inventory.Add(item);
        item.SetActive(false);
        Debug.Log(item.name + " added to inventory!");
    }
}
