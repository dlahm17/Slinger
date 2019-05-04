using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_ElevatorButton : InteractablePickup
{
    Animator myAnim;

    public override void Start()
    {
        base.Start();
        myAnim = GetComponent<Animator>();
    }
    public Elevator_Control myCtrl;
    public override void Interact()
    {
        base.Interact();
        myCtrl.activate();
        myAnim.SetBool("active", !myAnim.GetBool("active"));
    }
}
