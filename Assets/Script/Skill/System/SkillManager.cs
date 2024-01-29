using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject controller;
    public List<SkillScriptableObject> availableSkills = new List<SkillScriptableObject>();
    private Dictionary<string, SkillScriptableObject> skillDictionary = new Dictionary<string, SkillScriptableObject>();

    void Start()
    {
        InitializeSkillDictionary();
    }

    void InitializeSkillDictionary()
    {
        foreach (SkillScriptableObject skill in availableSkills)
        {
            skillDictionary.Add(skill.Name, skill);
        }
    }

    public void UseSkill(string skillName)
    {
        if (skillDictionary.ContainsKey(skillName))
        {
            SkillScriptableObject skill = skillDictionary[skillName];
            StartCoroutine(ActivateSkill(skill));
        }
        else
        {
            Debug.LogWarning("Skill not found: " + skillName);
        }
    }

    IEnumerator ActivateSkill(SkillScriptableObject skill)
    {
        if (!IsSkillOnCooldown(skill))
        {
            switch (skill.name)
            {
                case "Warrior_Swipe":
                    GameObject instantiatedPrefab = Instantiate(skill.LogicGameObject, controller.transform);
                    Debug.Log("Warrior_Swipe" + skill.Name);
                    break;
            }

            // Start cooldown
            StartCoroutine(CoolDownTimer(skill));
        }
        else
        {
            Debug.Log(skill.Name + " is on cooldown.");
        }

        yield return null;
    }

    bool IsSkillOnCooldown(SkillScriptableObject skill)
    {
        Debug.Log(skill.Name + " is on cooldown.");
        return false;
    }

    IEnumerator CoolDownTimer(SkillScriptableObject skill)
    {
        yield return new WaitForSecondsRealtime(skill.CoolDown);
    }
}
