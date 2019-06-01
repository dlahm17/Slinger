using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    new public string name = "New Item";
    public Sprite Icon = null;

    public bool isDefault = false;
    public bool isConsumable = false;

    public virtual void Use()
    {

        Debug.Log("Using " + name);
        
    }


    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
    //Types, modifiers, etc:
}
