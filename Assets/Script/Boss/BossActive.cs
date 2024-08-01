using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    public TriggerTracker triggerTracker;
    public GameObject bossSelf;
    public GameObject bossUI;
    public BossCore core;

    private void Update()
    {
        if(triggerTracker.triggered)
        {
            bossSelf.SetActive(true);
            bossUI.SetActive(true);
        }
        else
        {
            bossSelf.SetActive(false);
            bossUI.SetActive(false);
            core.bossHP = core.maxBossHP;
        }
    }
}
