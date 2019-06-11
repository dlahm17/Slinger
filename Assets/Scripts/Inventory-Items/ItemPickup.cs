using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : InteractablePickup {

    public Item item;
    public int ID;
    public override void Start()
    {
        base.Start();
        if (DataSave_Load.instance != null)
        {
            if (DataSave_Load.instance.GDat.pickupData[ID].haveBeenPickedUp == true)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            DataSave_Load.instance.GDat.pickupData[ID].haveBeenPickedUp = true;
            Destroy(gameObject);
        }
    }
}
