﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    #region singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }
    #endregion

    Equipment[] currentEquipment;
    Inventory inventory;
    

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        inventory = Inventory.instance;

    }
    public Equipment getequip(int slotIndex)
    {
        return currentEquipment[slotIndex];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;
        if(currentEquipment[slotIndex]!= null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }
}
