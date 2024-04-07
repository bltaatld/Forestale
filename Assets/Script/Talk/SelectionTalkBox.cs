using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTalkBox : MonoBehaviour
{
    public SelectManager selectManager;

    private void Start()
    {
        GameObject selectManger = GameObject.Find("PlayerSelectManager");
        if (selectManger != null)
        {
            selectManager = selectManger.GetComponent<SelectManager>();
        }
    }

    public void isYes()
    {
        if (selectManager != null)
        {
            selectManager.isYes();
        }
    }

    public void isNo()
    {
        if (selectManager != null)
        {
            selectManager.isNo();
        }
    }
}
