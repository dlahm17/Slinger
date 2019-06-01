using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSpecialAbility : InteractablePickup
{
    public SpecialAbilityObject mySpecialAbilityToGive;
    public int ID;
    public override void Start()
    {
        base.Start();
        if (DataSave_Load.instance.GDat.pickupData[ID].haveBeenPickedUp == true)
        {
            Interact();
            Destroy(gameObject);
        }
    }
    public override void Interact()
    {
        base.Interact();
        GameObject.FindGameObjectWithTag("Player").GetComponent<offlinePlayerShooting>().addSpAbility(mySpecialAbilityToGive);

        DataSave_Load.instance.GDat.pickupData[ID].haveBeenPickedUp = true;
        Destroy(gameObject);
    }
}
