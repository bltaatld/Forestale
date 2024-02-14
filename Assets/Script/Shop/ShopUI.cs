using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject[] parents;
    public ItemInfo[] itemInfos;
    public TextMeshProUGUI[] names;
    public TextMeshProUGUI[] costs;
    void Update()
    {
        SetHoldItems();

        for (int i = 0; i < itemInfos.Length; i++)
        {
            if (itemInfos[i] == null)
            {
                SetItemText(names[i], "Sold Out");
                SetItemText(costs[i], "Sold Out");
            }
            else
            {
                SetItemText(names[i], itemInfos[i].currentItemInfo.itemName);
                SetItemText(costs[i], itemInfos[i].currentItemInfo.cost.ToString());
            }
        }
    }

    void SetItemText(TextMeshProUGUI text, string value)
    {
        if (text != null)
        {
            text.text = value;
        }
    }

    public void SetHoldItems()
    {
        for(int i =0; i < parents.Length; i++)
        {
            Transform[] children = parents[i].GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child.CompareTag("Item"))
                {
                    itemInfos[i] = child.GetComponent<ItemInfo>();
                }
            }
        }
    }
}
