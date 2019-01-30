using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    public Equipment[] defaultEquipment = new Equipment[5];
    #region singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one equipment manager in the scene, deleting self");
            return;
        }
        instance = this;
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
        int i = 0;
        while (i < currentEquipment.Length) {
            if (currentEquipment[i] == null)
            {
                Debug.Log("Equipping default weapon");
                Equip(defaultEquipment[i]);
                Equipment oldItem = null;
                EquipmentUI.instance.UpdateUI(defaultEquipment[i], oldItem);
            }

            Debug.Log("Moving to next equipment");
            i++;
        }
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<offlinePlayerShooting>().setWeapon(currentEquipment[2] as Weapon);

    }

    public Equipment getWeapon()
    {
        if (currentEquipment[2] != null)
        {
            return currentEquipment[2];
        }
        return null;
        
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
