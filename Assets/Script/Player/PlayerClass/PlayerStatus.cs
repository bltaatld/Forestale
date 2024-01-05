using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    public int LV;
    public int EXP;
    public int HP;
    public int MP;
    public int WP;
    public int STR;
    public int DEX;
    public int INT;
    public int FOC;
}

[System.Serializable]
public class ExtraStatus
{
    public int Power;
    public int TrueDamage;
    public int DamageAginst;
    public int AttackSpeed;
    public int Defense;
    public int Agility;
    public int Magic;
    public int TrueMagic;
}
