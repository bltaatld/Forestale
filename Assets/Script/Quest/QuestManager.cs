using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject parent;
    public GameObject prefab;
    public List<QuestScriptableObject> quests = new List<QuestScriptableObject>();
    public List<ItemQuest> itemQuests = new List<ItemQuest>();
    public List<EnemyQuest> enemyQuests = new List<EnemyQuest>();
    public List<GameObject> currentObject = new List<GameObject>();

    public void UpdateCurrentQuest()
    {
        DestroyHoldObject(parent);

        Debug.Log(itemQuests.Count);
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].type == "Enemy")
            {
                continue;
            }

            if (quests[i].type == "Item")
            {
                GameObject instantiatedPrefab = Instantiate(prefab, parent.transform);
                currentObject.Add(instantiatedPrefab);

                instantiatedPrefab.GetComponent<QuestDescription>().titleText.text = quests[i].title;
                instantiatedPrefab.GetComponent<QuestDescription>().typeText.text = quests[i].type;

                instantiatedPrefab.GetComponent<QuestDescription>().currnetInfoText.text = itemQuests[i].currentInfo;
                instantiatedPrefab.GetComponent<QuestDescription>().isClear = itemQuests[i].isClear;
            }
        }

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].type == "Item")
            {
                continue;
            }
                
            if (quests[i].type == "Enemy")
            {
                GameObject instantiatedPrefab = Instantiate(prefab, parent.transform);
                currentObject.Add(instantiatedPrefab);

                instantiatedPrefab.GetComponent<QuestDescription>().titleText.text = quests[i].title;
                instantiatedPrefab.GetComponent<QuestDescription>().typeText.text = quests[i].type;

                instantiatedPrefab.GetComponent<QuestDescription>().currnetInfoText.text = enemyQuests[i].currentInfo;
                instantiatedPrefab.GetComponent<QuestDescription>().isClear = enemyQuests[i].isClear;
            }
        }
    }

    public void DestroyHoldObject(GameObject parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child != parent.transform)
            {
                currentObject.Clear();
                Destroy(child.gameObject);
            }
        }
    }
}
