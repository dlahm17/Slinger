using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item {
    public int goldGain = 0;
    public int HPGain = 0;
    public bool isPotion = false;
    public override void Use()
    {
        if (isPotion)
        {
            Debug.Log("consumable used");
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<player_Health>().heal(HPGain);
        Inventory.instance.giveGold(goldGain);
        RemoveFromInventory();
    }
}
