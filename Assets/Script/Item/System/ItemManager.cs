using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public PlayerInventory inventory;
    public List<ItemScriptableObject> availableItems = new List<ItemScriptableObject>();
    private Dictionary<string, ItemScriptableObject> itemDictionary = new Dictionary<string, ItemScriptableObject>();

    void Start()
    {
        InitializeItemDictionary();
    }

    void InitializeItemDictionary()
    {
        foreach (ItemScriptableObject item in availableItems)
        {
            itemDictionary.Add(item.itemName, item);
        }
    }

    public void UseItem(string itemName,int currentItem)
    {
        if (itemDictionary.ContainsKey(itemName))
        {
            ItemScriptableObject item = itemDictionary[itemName];
            ActivateItem(item, currentItem);
        }
        else
        {
            Debug.LogWarning("Item not found: " + itemName);
        }
    }

    public void ActivateItem(ItemScriptableObject item, int currentItem)
    {
        inventory.currentItem[currentItem] = item.LogicGameObject;
        Debug.Log(currentItem);
    }
}
