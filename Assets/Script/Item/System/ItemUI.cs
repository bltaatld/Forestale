using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemManager itemManager;
    public Image descriptionSprite;
    public Image[] iconSprite;
    public Image[] selectedIconSprite;
    public string[] itemName;
    public PlayerInventory playerInventory;
    public int currentSlot;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    private void Update()
    {
        UpdateIconSprites();
    }

    public void CurrentItemSet(int i)
    {
        UpdateItemDescription(i);
        currentSlot = i;
    }

    public void UseCurrentItem(int i)
    {
        itemManager.UseItem(itemName[currentSlot], i);
    }

    public void UpdateSeletedIcon(int i)
    {
        GameObject itemObject = playerInventory.inventory[currentSlot];
        ItemInfo itemInfo = itemObject.GetComponent<ItemInfo>();

        selectedIconSprite[i].sprite = itemInfo.currentItemInfo.icon;
    }

    public void ResetSeletedIcon()
    {
        for (int i = 0; i < selectedIconSprite.Length; i++)
        {
            selectedIconSprite[i].sprite = null;
        }    
    }

    void UpdateItemDescription(int i)
    {
        GameObject itemObject = playerInventory.inventory[i];
        ItemInfo itemInfo = itemObject.GetComponent<ItemInfo>();

        nameText.text = itemInfo.currentItemInfo.itemName;
        descriptionText.text = itemInfo.currentItemInfo.description;
        descriptionSprite.sprite = itemInfo.currentItemInfo.preview;
    }

    void UpdateIconSprites()
    {
        for (int i = 0; i < iconSprite.Length; i++)
        {
            if (i < playerInventory.inventory.Count)
            {
                GameObject itemObject = playerInventory.inventory[i];
                ItemInfo itemInfo = itemObject.GetComponent<ItemInfo>();

                itemName[i] = itemInfo.currentItemInfo.itemName;
                iconSprite[i].sprite = itemInfo.currentItemInfo.icon;
            }
        }
    }
}
