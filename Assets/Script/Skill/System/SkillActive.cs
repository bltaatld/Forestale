using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActive : MonoBehaviour
{
    public SkillManager skillManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Assume "Fireball" is the name of a skill in the SkillScriptableObject
            skillManager.UseSkill("Warrior_Swipe");
        }
    }
}
