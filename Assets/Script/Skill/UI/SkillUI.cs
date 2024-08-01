using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public SkillActive skillActive;
    public SkillManager skillManager;
    public PlayerController playerController;
    public TextMeshProUGUI textSP;
    public string currentSkillName;
    public float currentSP;
    public GameObject disActiveTarget;

    public void ShowSkillInfo(string currentName)
    {
        currentSkillName = currentName;
    }

    public void ActiveSkillButton(GameObject target)
    {
        if(currentSP > 0)
        {
            target.SetActive(true);
            currentSP -= 1f;
            targetDisActive();
        }
    }

    public void SetTarget(GameObject target)
    {
        disActiveTarget = target;
    }

    private void targetDisActive()
    {
        disActiveTarget.SetActive(false);
    }

    public void SetSkill(int skillOrder)
    {
        if (skillActive != null)
        {
            if (currentSkillName != null)
            {
                skillManager.imageOrder = skillOrder;
                skillManager.SetSkillIcon(currentSkillName);
            }
            skillActive.skillNames[skillOrder] = currentSkillName;
            Debug.Log("Skill "+ skillOrder + " is " + currentSkillName);
        }
        else
        {
            Debug.Log("Skill Name is null or empty");
        }
    }

    public void SetTextSP()
    {
        textSP.text = currentSP.ToString();
    }

    public void ResetSP()
    {
        //currentSP = playerController.playerStatus.LV;
        currentSP = 8;
    }

    private void Update()
    {
        SetTextSP();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            gameObject.SetActive(false);
        }
    }
}
