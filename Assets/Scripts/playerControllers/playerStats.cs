using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats :  characterStats {

    EquipmentManager statManage;
    offlinePlayerMovement playMove;
	// Use this for initialization
	void Start () {
        playMove = GetComponent<offlinePlayerMovement>();
        statManage = EquipmentManager.instance;
        statManage.onEquipmentChanged += OnEquipmentChanged;
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
            playMove.setSpeed();
        }
        if(oldItem != null)
        {
            armor.removeModifier(oldItem.armorModifier);
            damage.removeModifier(oldItem.armorModifier);
            magicArmor.removeModifier(oldItem.mArmorModifier);
            magicDamage.removeModifier(oldItem.mDamageModifier);
            stealth.removeModifier(oldItem.stealthModifier);
            speed.removeModifier(oldItem.speedModifier);
            playMove.setSpeed();
        }
    }
}
