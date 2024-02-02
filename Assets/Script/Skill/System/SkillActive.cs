using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActive : MonoBehaviour
{
    public SkillManager skillManager;
    public string[] skillNames;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            skillManager.UseSkill(skillNames[0]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            skillManager.UseSkill(skillNames[1]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            skillManager.UseSkill(skillNames[2]);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            skillManager.UseSkill(skillNames[3]);
        }
    }
}
