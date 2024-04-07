using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject player;
    public Image[] images;
    public int imageOrder;
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

    public void SetSkillIcon(string skillName)
    {
        SkillScriptableObject skill = skillDictionary[skillName];
        ActiveSkillIcon(skill);
    }

    public void ActiveSkillIcon(SkillScriptableObject skill)
    {
        switch (skill.name)
        {
            case "Debug":
                Debug.Log("Debug Skill Icon Active");
                break;
            case "Warrior_Swipe":
                images[imageOrder].sprite = skill.Icon;
                break;
            case "Warrior_Whip":
                images[imageOrder].sprite = skill.Icon;
                break;
            case "Archer_Field":
                images[imageOrder].sprite = skill.Icon;
                break;
            case "Archer_Arrow":
                images[imageOrder].sprite = skill.Icon;
                break;
            case "Magic_Fire":
                images[imageOrder].sprite = skill.Icon;
                break;
            case "Magic_Shot":
                images[imageOrder].sprite = skill.Icon;
                break;
            case "Rogue_Dart":
                images[imageOrder].sprite = skill.Icon;
                break;
            case "Rogue_SeedDart":
                images[imageOrder].sprite = skill.Icon;
                break;
        }
    }

    IEnumerator ActivateSkill(SkillScriptableObject skill)
    {
        if (!IsSkillOnCooldown(skill))
        {
            switch (skill.name)
            {
                case "Debug":
                    Debug.Log("Debug Skill Active");
                    break;
                case "Warrior_Swipe":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        Instantiate(skill.LogicGameObject, player.transform);
                        Debug.Log(skill.Name);
                    }
                    break;
                case "Archer_Field":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        GameObject archerField = Instantiate(skill.LogicGameObject);
                        archerField.transform.position = player.transform.position;
                        Debug.Log(skill.Name);
                    }
                    break;
                case "Magic_Shot":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        Instantiate(skill.LogicGameObject, player.transform);
                        Debug.Log(skill.Name);
                    }
                    break;
                case "Magic_Fire":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        Instantiate(skill.LogicGameObject, player.transform);
                        Debug.Log(skill.Name);
                    }
                    break;
                case "Rogue_Dart":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        Instantiate(skill.LogicGameObject, player.transform);
                        Debug.Log(skill.Name);
                    }
                    break;
                case "Rogue_SeedDart":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        Instantiate(skill.LogicGameObject, player.transform);
                        Debug.Log(skill.Name);
                    }
                    break;
                case "Warrior_Whip":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        Instantiate(skill.LogicGameObject, player.transform);
                        Debug.Log(skill.Name);
                    }
                    break;
                case "Archer_Arrow":
                    if (player.GetComponent<PlayerController>().playerStatus.MP >= skill.manaCost)
                    {
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        player.GetComponent<PlayerController>().playerStatus.MP -= skill.manaCost;
                        Instantiate(skill.LogicGameObject, player.transform);
                        Debug.Log(skill.Name);
                    }
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
