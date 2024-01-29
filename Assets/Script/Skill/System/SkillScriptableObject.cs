using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/Skill", order = 1)]
public class SkillScriptableObject : ScriptableObject
{
    public GameObject LogicGameObject;
    public string Name;
    public float CoolDown;
    public string Type;
}
