using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item {
    public int goldGain = 0;
    public int HPGain = 0;
   
    public override void Use()
    {
        base.Use();
        GameObject.FindGameObjectWithTag("Player").GetComponent<m_Health>().heal(HPGain);
        Inventory.instance.giveGold(goldGain);
        RemoveFromInventory();
    }
}
