using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour {
    EquipmentManager Equipment;
    public Transform itemsParent;

    public EquipSlot Headgear; //0
    
    public EquipSlot Armor; //1
    public EquipSlot Weapon;//2
    public EquipSlot Amulet;//3
    public EquipSlot Boots;//4
	// Use this for initialization
	void Start () {
        Equipment = EquipmentManager.instance;
        Equipment.onEquipmentChanged += UpdateUI;
	}
	

    void UpdateUI(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            if ((int)newItem.equipSlot == 0)
            {
                Headgear.AddItem(newItem);
            }
            if ((int)newItem.equipSlot == 1)
            {
                Armor.AddItem(newItem);
            }
            if ((int)newItem.equipSlot == 2)
            {
                Weapon.AddItem(newItem);
            }
            if ((int)newItem.equipSlot == 3)
            {
                Amulet.AddItem(newItem);
            }
            if ((int)newItem.equipSlot == 4)
            {
                Boots.AddItem(newItem);
            }
        }
    }
}
