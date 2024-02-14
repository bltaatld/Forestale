using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public int ID;
    public int cost;
    public Sprite icon;
    public Sprite preview;
    public GameObject LogicGameObject;
    public string itemName;
    public string description;
    public ItemStatus itemStatus;
    public ItemExtraStatus itemExtraStatus;
}


[System.Serializable]
public class ItemStatus
{
    public int STR;
    public int DEX;
    public int INT;
    public int FOC;
}

[System.Serializable]
public class ItemExtraStatus
{
    public int Power;
    public int TrueDamage;
    public int IgnoreAginst;
    public int AttackSpeed;
    public int Defense;
    public int Agility;
    public int Magic;
    public int TrueMagic;
}