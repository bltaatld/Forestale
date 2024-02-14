using UnityEngine;

[CreateAssetMenu(fileName = "NewSeed", menuName = "ScriptableObjects/Seed", order = 1)]
public class SeedScriptableObject : ScriptableObject
{
    public Sprite Icon;
    public Sprite Preview;
    public GameObject LogicGameObject;
    public string Name;
    public string Description;
    public string Type;
    public int seedCost;
}
