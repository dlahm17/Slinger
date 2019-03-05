using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSpecialAbility : InteractablePickup
{
    public SpecialAbilityObject mySpecialAbilityToGive;
    public override void Interact()
    {
        base.Interact();
        GameObject.FindGameObjectWithTag("Player").GetComponent<offlinePlayerShooting>().addSpAbility(mySpecialAbilityToGive);
        Destroy(gameObject);
    }
}
