using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour {
    Item item;
    public Image icon;
    public Button RemoveButton;
    public Text mytxt;

    public void OnEnable()
    {
        if (item != null)
        {
            mytxt.text = item.name;
        }
    }
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        RemoveButton.interactable = true;
        mytxt.text = item.name;
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
        Inventory.instance.Remove(item);
    }
    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
            
        }
    }
}
