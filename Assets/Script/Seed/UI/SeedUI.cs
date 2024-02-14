using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SeedUI : MonoBehaviour
{
    public PlayerController controller;
    public SeedManager seedManager;
    public GameObject currentSeed;
    public GameObject targetParent;
    public GameObject descriptionTarget;

    public Image[] iconSprite;
    public string[] seedName;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image descriptionSprite;
    public Image descriptionIconSprite;

    public PlayerSeed playerSeed;
    public int currentSlot;

    private void Update()
    {
        UpdateIconSprites();
    }

    private void OnEnable()
    {
        CurrentSeedDisplay();
    }

    public void CurrentSeedDisplay()
    {
        foreach (Transform child in targetParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < controller.systemValue.Seed; i++)
        {
            Instantiate(currentSeed, targetParent.transform);
        }
    }

    public void SetSeed()
    {
        if (controller.systemValue.Seed > 0)
        {
            seedManager.UseSeed(seedName[currentSlot]);
            CurrentSeedDisplay();
            playerSeed.ActiveCurrentSeed();
        }
    }

    public void CurrentItemSet(int i)
    {
        UpdateItemDescription(i);
        currentSlot = i;
    }

    void UpdateItemDescription(int i)
    {
        GameObject seedObject = playerSeed.seedInventory[i];
        SeedInfo seedInfo = seedObject.GetComponent<SeedInfo>();

        nameText.text = seedInfo.currentSeedInfo.Name;
        descriptionText.text = seedInfo.currentSeedInfo.Description;
        descriptionSprite.sprite = seedInfo.currentSeedInfo.Preview;
        descriptionIconSprite.sprite = seedInfo.currentSeedInfo.Icon;

        foreach (Transform child in descriptionTarget.transform)
        {
            Destroy(child.gameObject);
        }

        for (int value = 0; value < seedInfo.currentSeedInfo.seedCost; value++)
        {
            Debug.Log("asdasd");
            Instantiate(currentSeed, descriptionTarget.transform);
        }
    }

    void UpdateIconSprites()
    {
        for (int i = 0; i < controller.systemValue.Seed; i++)
        {
            if (i < playerSeed.seedInventory.Count)
            {
                GameObject seedObject = playerSeed.seedInventory[i];
                SeedInfo seedInfo = seedObject.GetComponent<SeedInfo>();

                seedName[i] = seedInfo.currentSeedInfo.Name;
                iconSprite[i].sprite = seedInfo.currentSeedInfo.Icon;
            }
        }
    }
}
