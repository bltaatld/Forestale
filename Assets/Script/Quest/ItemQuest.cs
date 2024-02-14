using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemQuest : MonoBehaviour
{
    [Header("QuestSystem")]
    public ItemQuest self;
    public QuestScriptableObject questScript;
    public QuestManager questManager;

    [Header("PlayerIfno")]
    public PlayerController controller;
    public PlayerInventory playerInventory;
    public ItemInfo needItem;

    [Header("Reperence")]
    public int itemAmount;
    public int EXP;

    [Header("Dialogue")]
    public SelectManager selectManager;
    public Dialogue startDialogue;
    public GameObject onQuest;
    public GameObject endDialogue;

    [Header("Value")]
    public int currentItemAmount;
    public string currentInfo;
    public bool isClear;
    public bool isFirst;

    public void CheckCurrentItem()
    {
        Debug.Log(playerInventory.inventory);
    }

    void Update()
    {
        currentInfo = "Find" + " '" + needItem.currentItemInfo.itemName + "' " + currentItemAmount + " / " + itemAmount;

        if (startDialogue.triggerActive || onQuest.activeSelf)
        {
            if (onQuest.GetComponent<Dialogue>().isTalk)
            {
                CheckQuest();
            }

            if (selectManager.selection == true)
            {
                CheckQuest();
                selectManager.selection = false;
            }
        }
    }

    public void CheckQuest()
    {
        string targetObjectName = needItem.name;
        currentItemAmount = FindObjectsByName(targetObjectName);
        Debug.Log(targetObjectName);

        if (currentItemAmount >= itemAmount)
        {
            if (!isClear)
            {
                isClear = true;
                Debug.Log("Found enough objects: " + currentItemAmount);
                controller.playerStatus.currentEXP += EXP;

                onQuest.SetActive(false);
                endDialogue.SetActive(true);
                startDialogue.enabled = false;
            }
        }
        else
        {
            if (!isFirst)
            {
                startDialogue.enabled = false;
                questManager.quests.Add(questScript);
                questManager.itemQuests.Add(self);
                isFirst = true;
                onQuest.SetActive(true);
            }

            Debug.Log("Not enough objects found. Found: " + currentItemAmount + ", Required: " + itemAmount);
            endDialogue.SetActive(false);
        }
    }

    int FindObjectsByName(string objectName)
    {
        return playerInventory.inventory.Count(obj => obj.name == objectName);
    }
}
