using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour {

    #region singleton
    public static EquipmentUI instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one equipmentUI");
            return;
            
        }
        instance = this;
    }
    #endregion
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
        if(Equipment == null)
        {
            Debug.Log("Equipment manager is not available");
        }
        Equipment.onEquipmentChanged += UpdateUI;
	}
	

    public void UpdateUI(Equipment newItem, Equipment oldItem)
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
