using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescription : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI currnetInfoText;
    public GameObject description;

    public bool isClear;
    public GameObject clear;
    public GameObject notClear;

    private void Update()
    {
        if (isClear)
        {
            notClear.SetActive(false);
            clear.SetActive(true);
        }
    }

    public void PopupDescription()
    {
        description.SetActive(!description.activeSelf);
    }
}
