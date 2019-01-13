using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot { Head, Armor, Weapon, Amulet, Boots}



[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
   

    public int armorModifier = 0;
    public int damageModifier = 0;
    public int mArmorModifier = 0;
    public int mDamageModifier = 0;
    public int stealthModifier = 0;
    public int speedModifier = 0;


    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
