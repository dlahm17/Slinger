using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats :  characterStats {

    EquipmentManager statManage;
    offlinePlayerMovement playMove;
    public displayPlayerStats display;
	// Use this for initialization
	void Start () {
        playMove = GetComponent<offlinePlayerMovement>();
        statManage = EquipmentManager.instance;
        statManage.onEquipmentChanged += OnEquipmentChanged;
	}
    public void resetAllStats()
    {
        armor.removeAllModifiers();
        damage.removeAllModifiers();
        magicArmor.removeAllModifiers();
        magicDamage.removeAllModifiers();
        stealth.removeAllModifiers();
        speed.removeAllModifiers();
        if(playMove == null)
        {
            playMove = GetComponent<offlinePlayerMovement>();
        }
        playMove.setSpeed();
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            armor.removeModifier(oldItem.armorModifier);
            damage.removeModifier(oldItem.damageModifier);
            magicArmor.removeModifier(oldItem.mArmorModifier);
            magicDamage.removeModifier(oldItem.mDamageModifier);
            stealth.removeModifier(oldItem.stealthModifier);
            speed.removeModifier(oldItem.speedModifier);
            playMove.setSpeed();
        }
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
        display.onChange();
    }
    public void updateUI()
    {
        display.onChange();
    }
}
