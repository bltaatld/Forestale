using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatApply : MonoBehaviour
{
    public ItemScriptableObject item;
    public GameObject player;


    private void OnEnable()
    {
        player = GameObject.Find("Player");
        PlayerController controller = player.GetComponent<PlayerController>();

        controller.playerStatus.STR += item.itemStatus.STR;
        controller.playerStatus.DEX += item.itemStatus.DEX;
        controller.playerStatus.INT += item.itemStatus.INT;
        controller.playerStatus.FOC += item.itemStatus.FOC;

        controller.extraPlayerStatus.Power += item.itemExtraStatus.Power;
        controller.extraPlayerStatus.TrueDamage += item.itemExtraStatus.TrueDamage;
        controller.extraPlayerStatus.IgnoreAginst += item.itemExtraStatus.IgnoreAginst;
        controller.extraPlayerStatus.AttackSpeed += item.itemExtraStatus.AttackSpeed;
        controller.extraPlayerStatus.Defense += item.itemExtraStatus.Defense;
        controller.extraPlayerStatus.Agility += item.itemExtraStatus.Agility;
        controller.extraPlayerStatus.Magic += item.itemExtraStatus.Magic;
        controller.extraPlayerStatus.TrueMagic += item.itemExtraStatus.TrueMagic;
    }

    private void OnDestroy()
    {
        player = GameObject.Find("Player");
        PlayerController controller = player.GetComponent<PlayerController>();

        controller.playerStatus.STR -= item.itemStatus.STR;
        controller.playerStatus.DEX -= item.itemStatus.DEX;
        controller.playerStatus.INT -= item.itemStatus.INT;
        controller.playerStatus.FOC -= item.itemStatus.FOC;

        controller.extraPlayerStatus.Power -= item.itemExtraStatus.Power;
        controller.extraPlayerStatus.TrueDamage -= item.itemExtraStatus.TrueDamage;
        controller.extraPlayerStatus.IgnoreAginst -= item.itemExtraStatus.IgnoreAginst;
        controller.extraPlayerStatus.AttackSpeed -= item.itemExtraStatus.AttackSpeed;
        controller.extraPlayerStatus.Defense -= item.itemExtraStatus.Defense;
        controller.extraPlayerStatus.Agility -= item.itemExtraStatus.Agility;
        controller.extraPlayerStatus.Magic -= item.itemExtraStatus.Magic;
        controller.extraPlayerStatus.TrueMagic -= item.itemExtraStatus.TrueMagic;
    }
}
