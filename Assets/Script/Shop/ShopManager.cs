using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{
    public ThrowObject playerThrow;
    public Dialogue dialogue;
    public Transform[] transforms;
    public GameObject[] canHoldItems;
    public int honerTriggerValue;

    public GameObject currentHold;
    public GameObject selectUI;
    public GameObject talkUI;
    public GameObject honerTalkUI;
    public GameObject itemParent;
    
    public PlayerController playerController;
    public SelectManager selectManager;
    
    private GameObject currentChild;
    private int currentCost;
    private bool isHolding;

    private void OnEnable()
    {
        playerThrow = GameObject.FindWithTag("Player").GetComponent<ThrowObject>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        SpwanItems();
    }

    public void Update()
    {
        CheckPlayerHolding();

        if (dialogue.triggerActive)
        {
            playerThrow.isShop = true;

            if (dialogue.isTalk)
            {
                CheckChildObjects(currentHold);

                if (selectManager.selection == true)
                {
                    BuyObject(currentCost);
                    selectManager.selection = false;
                }
            }
        }

        if (!dialogue.triggerActive)
        {
            playerThrow.isShop = false;
        }
    }

    public void SpwanItems()
    {
        for(int i =0; i < canHoldItems.Length; i++)
        {
            GameObject spawnObject = Instantiate(canHoldItems[i], transforms[i]);
            spawnObject.transform.position = transforms[i].position;
        }
    }

    public void CheckPlayerHolding()
    {
        if (playerThrow.itemHolding)
        {
            isHolding = true;
            selectUI.SetActive(true);
            talkUI.SetActive(false);
        }

        if (!playerThrow.itemHolding)
        {
            isHolding = false;
            selectUI.SetActive(false);

            if(honerTriggerValue > playerController.systemValue.Honor)
            {
                talkUI.SetActive(false);
                honerTalkUI.SetActive(true);
            }

            if (honerTriggerValue < playerController.systemValue.Honor)
            {
                talkUI.SetActive(true);
                honerTalkUI.SetActive(false);
            }
        }
    }

    public void BuyObject(int cost)
    {
        if (playerController.systemValue.Amber > cost)
        {
            playerController.systemValue.Amber -= cost;

            GameObject item = Instantiate(currentChild, itemParent.transform);
            item.GetComponent<BoxCollider2D>().enabled = true;
            item.transform.position = playerController.transform.position;
            DestroyHoldObject(currentHold);
            Debug.Log("Buy!");
        }
    }

    public void SteelObject()
    {
        GameObject item = Instantiate(currentChild, itemParent.transform);
        item.GetComponent<BoxCollider2D>().enabled = true;
        item.transform.position = playerController.transform.position;

        playerController.systemValue.Honor -= 1;
        DestroyHoldObject(currentHold);
        Debug.Log("Steel!");
    }

    public void DestroyHoldObject(GameObject parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.CompareTag("Rock"))
            {
                Destroy(child.gameObject);
            }    
        }
    }

    public void CheckChildObjects(GameObject parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.CompareTag("Item"))
            {
                currentChild = child.gameObject;
                currentCost = child.GetComponent<ItemInfo>().currentItemInfo.cost;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerThrow.isShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isHolding)
            {
                CheckChildObjects(currentHold);
                SteelObject();
            }
            playerThrow.isShop = false;
        }
    }
}
