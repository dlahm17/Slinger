using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour {
    Item item;
    public Image icon;
    public Button RemoveButton;
    public Text mytxt;
    int myEquipSlot;
 
    public void OnEnable()
    {
        if (item != null)
        {
            mytxt.text = item.name;
        }
    }

    public void AddItem(Equipment newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        RemoveButton.interactable = true;
        mytxt.text = item.name;
        myEquipSlot = (int)newItem.equipSlot;
    }
    

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        RemoveButton.interactable = false;
        mytxt.text = "";
    }
    public void OnRemoveButton()
    {
        Debug.Log("Attempting Removal");
        
         EquipmentManager.instance.Unequip(myEquipSlot);
         ClearSlot();
        
        
        
    }
}

