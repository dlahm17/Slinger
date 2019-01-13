using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats :  characterStats {

	// Use this for initialization
	void Start () {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}
	

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.addModifier(newItem.armorModifier);
            damage.addModifier(newItem.damageModifier);
            magicArmor.addModifier(newItem.mArmorModifier);
            magicDamage.addModifier(newItem.mDamageModifier);
            stealth.addModifier(newItem.stealthModifier);
            speed.addModifier(newItem.speedModifier);
        }
        if(oldItem != null)
        {
            armor.removeModifier(oldItem.armorModifier);
            damage.removeModifier(oldItem.armorModifier);
            magicArmor.removeModifier(newItem.mArmorModifier);
            magicDamage.removeModifier(newItem.mDamageModifier);
            stealth.removeModifier(newItem.stealthModifier);
            speed.removeModifier(newItem.speedModifier);
        }
    }
}
