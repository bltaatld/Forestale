using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/Skill", order = 1)]
public class SkillScriptableObject : ScriptableObject
{
    public Sprite Icon;
    public GameObject LogicGameObject;
    public string Name;
    public float CoolDown;
    public int manaCost;
    public string Type;
}
