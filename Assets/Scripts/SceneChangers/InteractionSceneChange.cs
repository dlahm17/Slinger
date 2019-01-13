using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSceneChange : InteractablePickup
{
    /// <summary>
    /// This is the object itself that will send the door to the controller, which will handle scene changes.
    /// </summary>
    public Door myDoor;
    public Transform myTransOnChangingScene;
    

    public override void Interact()
    {
        base.Interact();
        changeScene();
    }
    void changeScene()
    {
        PlayerDoorIDController.instance.changeTheScene(myDoor);
    }
}
