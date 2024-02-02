using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    public int LV;
    public int currentEXP;
    public int needEXP;
    public float HP;
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
    public int IgnoreAginst;
    public int AttackSpeed;
    public int Defense;
    public int Agility;
    public int Magic;
    public int TrueMagic;
}

[System.Serializable]
public class DamagedOutput
{
    public float NormalDamage;
    public float MagicDamage;
    public float HitDamage;
    public float CriticalChance;
    public float NormalDamagePercentage;
    public float MagicDamagePercentage;
}

[System.Serializable]
public class SystemValue
{
    public int Amber;
    public int Honor;
}