using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    [Header("Logic Value")]
    public int currentLP;
    private float heartResult;

    [Header("Component")]
    public PlayerController playerController;
    public HealthHeartBar healthHeartBar;
    #region TMPtexts
    public TextMeshProUGUI LVtext;
    public TextMeshProUGUI EXPtext;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI MPtext;
    public TextMeshProUGUI WPtext;
    public TextMeshProUGUI STRtext;
    public TextMeshProUGUI DEXtext;
    public TextMeshProUGUI INTtext;
    public TextMeshProUGUI FOCtext;
    public TextMeshProUGUI Powertext;
    public TextMeshProUGUI TrueDamagetext;
    public TextMeshProUGUI IgnoreAginsttext;
    public TextMeshProUGUI AttackSpeedtext;
    public TextMeshProUGUI Defensetext;
    public TextMeshProUGUI Agilitytext;
    public TextMeshProUGUI Magictext;
    public TextMeshProUGUI TrueMagictext;
    public TextMeshProUGUI NormalDamage;
    public TextMeshProUGUI MagicDamage;
    public TextMeshProUGUI HitDamage;
    public TextMeshProUGUI CriticalChance;
    public TextMeshProUGUI SPtext;
    public TextMeshProUGUI AmberText;
    public TextMeshProUGUI InGameLVyext;
    #endregion

    private void Start()
    {
        CaculateStatus();
        healthHeartBar.DrawHearts();
    }

    private void Update()
    {
        TextOutput();
    }

    public void ResetLP()
    {
        currentLP = playerController.playerStatus.LV;
    }

    public void CaculateStatus()
    {

        //HP
        heartResult = 250 + (100 * playerController.playerStatus.LV) / 2;
        playerController.playerStatus.HP = ((int)heartResult / 100) * 2;
        playerController.playerMaxHP = ((int)heartResult / 100) * 2;

        //MP
        float manaResult = 100 + (100 * playerController.playerStatus.LV) / 1.5f;
        playerController.playerStatus.MP = (int)manaResult;
        playerController.playerMaxMP = (int)manaResult;

        //WP
        playerController.playerStatus.WP = (int)heartResult;

        //Normal Damage
        float trueDamage = playerController.extraPlayerStatus.TrueDamage;
        float power = playerController.extraPlayerStatus.Power;

        float result = 1 + (trueDamage + (trueDamage * (power / 100f)));
        float percentage = trueDamage * (power / 100f);

        playerController.damagedOutput.NormalDamage = result;
        playerController.damagedOutput.NormalDamagePercentage = percentage;

        //Magic Damage
        float trueMagic = playerController.extraPlayerStatus.TrueMagic;
        float magic = playerController.extraPlayerStatus.Magic;

        float magicResult = 1 + (trueMagic + (trueMagic * (magic / 100f)));
        float magicPercentage = trueMagic * (magic / 100f);

        playerController.damagedOutput.MagicDamage = magicResult;
        playerController.damagedOutput.MagicDamagePercentage = magicPercentage;

        //Critical Chance
        playerController.damagedOutput.CriticalChance = power;
    }

    public void HitDamageCaculate()
    {
        float defence = playerController.extraPlayerStatus.Defense;

        if (defence <= 20)
        {
            defence *= 1.3f;
            playerController.damagedOutput.HitDamage = defence;
        }
        else if (playerController.extraPlayerStatus.Defense <= 40)
        {
            defence *= 1.25f;
            playerController.damagedOutput.HitDamage = defence;
        }
        else if (playerController.extraPlayerStatus.Defense <= 80)
        {
            defence *= 1.2f;
            playerController.damagedOutput.HitDamage = defence;
        }
        else
        {
            defence *= 1.1f;
            playerController.damagedOutput.HitDamage = defence;
        }
    }

    public void UpgradeSTR()
    {
        if (currentLP > 0)
        {
            Debug.Log("STR Upgrade");
            playerController.playerStatus.STR += 1;
            playerController.extraPlayerStatus.Power += 2;
            playerController.extraPlayerStatus.TrueDamage += 1;
            playerController.extraPlayerStatus.Defense += 2;
            HitDamageCaculate();
            currentLP -= 1;
        }
        else
        {
            Debug.Log("Not enough SP");
        }
    }

    public void UpgradeDEX()
    {
        if (currentLP > 0)
        {
            Debug.Log("DEX Upgrade");
            playerController.playerStatus.DEX += 1;

            //Agility Upgrade
            playerController.extraPlayerStatus.Agility += 1;
            playerController.maxMoveSpeed += 0.1f;
 
            playerController.extraPlayerStatus.TrueDamage += 3;
            currentLP -= 1;
        }
        else
        {
            Debug.Log("Not enough SP");
        }
    }

    public void UpgradeINT()
    {
        if (currentLP > 0)
        {
            Debug.Log("INT Upgrade");

            playerController.playerStatus.INT += 1;
            playerController.extraPlayerStatus.Magic += 2;
            playerController.extraPlayerStatus.TrueMagic += 1;
            currentLP -= 1;
        }
        else
        {
            Debug.Log("Not enough SP");
        }
    }

    public void UpgradeFOC()
    {
        if (currentLP > 0)
        {
            Debug.Log("FOC Upgrade");

            playerController.playerStatus.FOC += 1;
            playerController.extraPlayerStatus.AttackSpeed += 2;
            playerController.extraPlayerStatus.TrueMagic += 3;
            currentLP -= 1;
        }
        else
        {
            Debug.Log("Not enough SP");
        }
    }

    public void TextOutput()
    {
        SPtext.text = currentLP.ToString();
        LVtext.text = playerController.playerStatus.LV.ToString();
        EXPtext.text = playerController.playerStatus.needEXP.ToString();
        HPtext.text = $"{heartResult} | {heartResult / 100} Heart";
        MPtext.text = playerController.playerStatus.MP.ToString();
        WPtext.text = playerController.playerStatus.WP.ToString();
        STRtext.text = playerController.playerStatus.STR.ToString();
        DEXtext.text = playerController.playerStatus.DEX.ToString();
        INTtext.text = playerController.playerStatus.INT.ToString();
        FOCtext.text = playerController.playerStatus.FOC.ToString();
        Powertext.text = playerController.extraPlayerStatus.Power.ToString();
        TrueDamagetext.text = playerController.extraPlayerStatus.TrueDamage.ToString();
        TrueMagictext.text = playerController.extraPlayerStatus.TrueMagic.ToString();
        IgnoreAginsttext.text = playerController.extraPlayerStatus.IgnoreAginst.ToString();
        AttackSpeedtext.text = playerController.extraPlayerStatus.AttackSpeed.ToString();
        Defensetext.text = playerController.extraPlayerStatus.Defense.ToString();
        Agilitytext.text = playerController.extraPlayerStatus.Agility.ToString() + "%";
        Magictext.text = playerController.extraPlayerStatus.Magic.ToString();
        NormalDamage.text = playerController.damagedOutput.NormalDamage.ToString() + " (" + playerController.damagedOutput.NormalDamagePercentage.ToString() + "%)";
        MagicDamage.text = playerController.damagedOutput.MagicDamage.ToString() + " (" + playerController.damagedOutput.MagicDamagePercentage.ToString() + "%)"; ;
        HitDamage.text = playerController.damagedOutput.HitDamage.ToString() + "%";
        CriticalChance.text = playerController.damagedOutput.CriticalChance.ToString()+ "%";
        AmberText.text = playerController.systemValue.Amber.ToString();
        InGameLVyext.text = playerController.playerStatus.LV.ToString();
    }
}