using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : InteractablePickup
{
    //key to door is the key that this door uses.  The player needs to have this key in their key inventory to open the door.
    public Key KeyToDoor;
    public AudioSource negativeAudio;

    public NPCDialogueNode positiveNode;
    public NPCDialogueNode negativeNode;
    public override void Interact()
    {
        base.Interact();
        if(Inventory.instance.queryKey(KeyToDoor) == true)
        {
            unlock();
        }
        else
        {
            negativeAudio.PlayOneShot(negativeAudio.clip, 1);
            DialogueController.instance.ActivateDialogue(negativeNode.myDialogue[0], negativeNode);
        }
    }

    void unlock()
    {
        Debug.Log("Unlocked");
        DialogueController.instance.ActivateDialogue(positiveNode.myDialogue[0], positiveNode);
    }
}
