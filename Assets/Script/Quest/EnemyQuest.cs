using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyQuest : MonoBehaviour
{
    [Header("QuestSystem")]
    public EnemyQuest self;
    public QuestScriptableObject questScript;
    public QuestManager questManager;

    [Header("PlayerIfno")]
    public PlayerController controller;
    public PlayerInventory playerInventory;
    public GameObject[] EnemyObject;

    [Header("Reperence")]
    public int enemyAmount;
    public int EXP;

    [Header("Dialogue")]
    public SelectManager selectManager;
    public Dialogue startDialogue;
    public GameObject onQuest;
    public GameObject endDialogue;

    [Header("Value")]
    public string currentInfo;
    public bool isClear;
    public bool isFirst;
    public bool isAllKill;

    public void CheckCurrentItem()
    {
        Debug.Log(playerInventory.inventory);
    }

    void Update()
    {
        currentInfo = "Kill";

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
        for (int i = 0; i < EnemyObject.Length; i++)
        {
            if (EnemyObject[i].activeSelf)
            {
                isAllKill = false;
                break;
            }
            else
            {
                isAllKill = true;
            }
        }

        if (isAllKill)
        {
            if (!isClear)
            {
                isClear = true;
                Debug.Log("All Kill Enemy");
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
                startDialogue.triggerActive = false;
                startDialogue.enabled = false;
                questManager.quests.Add(questScript);
                questManager.enemyQuests.Add(self);
                isFirst = true;
                onQuest.SetActive(true);
            }

            Debug.Log("Not Enough Enemy kill");
            endDialogue.SetActive(false);
        }
    }
}
