using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SeedManager : MonoBehaviour
{
    public GameObject currentTarget;
    public GameObject currentSprite;
    public PlayerSeed seedInventory;
    public PlayerController controller;
    public List<SeedScriptableObject> availableSeeds = new List<SeedScriptableObject>();
    private Dictionary<string, SeedScriptableObject> seedDictionary = new Dictionary<string, SeedScriptableObject>();

    void Start()
    {
        seedInventory = GameObject.Find("Player").GetComponent<PlayerSeed>();
        InitializeSeedDictionary();
    }

    void InitializeSeedDictionary()
    {
        foreach (SeedScriptableObject seed in availableSeeds)
        {
            seedDictionary.Add(seed.Name, seed);
        }
    }

    public void ResetSeed()
    {
        foreach (Transform child in currentTarget.transform)
        {
            if (child.gameObject.name != "SeedButton")
            {
                Destroy(child.gameObject);
            }
        }

        controller.systemValue.Seed = controller.playerMaxSeed;
    }

    public void UseSeed(string seedName)
    {
        if (seedDictionary.ContainsKey(seedName))
        {
            SeedScriptableObject seed = seedDictionary[seedName];
            ActivateSeed(seed);
        }
        else
        {
            Debug.LogWarning("Item not found: " + seedName);
        }
    }

    public void ActivateSeed(SeedScriptableObject seed)
    {
        switch (seed.name)
        {
            case "Debug":
                Debug.Log("Debug Item Active");
                break;

            case "TestSeed":
                if (controller.systemValue.Seed - seed.seedCost > 0)
                {
                    seedInventory.currentSeed.Add(seed.LogicGameObject);

                    GameObject object1 = Instantiate(currentSprite, currentTarget.transform);
                    object1.GetComponent<Image>().sprite = seed.Icon;

                    controller.systemValue.Seed -= seed.seedCost;
                    Debug.Log("TestSeed Update");
                }
                break;

            case "TestSeed 1":
                if (controller.systemValue.Seed - seed.seedCost > 0)
                {
                    seedInventory.currentSeed.Add(seed.LogicGameObject);

                    GameObject object2 = Instantiate(currentSprite, currentTarget.transform);
                    object2.GetComponent<Image>().sprite = seed.Icon;

                    controller.systemValue.Seed -= seed.seedCost;
                    Debug.Log("TestSeed 1 Update");
                }
                break;
        }
    }
}
