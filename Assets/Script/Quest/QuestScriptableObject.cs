using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "ScriptableObjects/Quest", order = 1)]
public class QuestScriptableObject : ScriptableObject
{
    public string title;
    public string type;
    public string description;
}
