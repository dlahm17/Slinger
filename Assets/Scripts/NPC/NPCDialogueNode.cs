using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueNode : InteractablePickup
{
    //The node will send dialogue items to the controller.  The node can hold up to 100 items.
    bool active = false;
    DialogueController dialogueCtrl;


    public Dialogue[] myDialogue = new Dialogue[100];
    int i = 0;

    //These variables set the distance the player can move from the speaker before the dialogue just ends.
    public float maxDistFromPlayer = 5f;
    GameObject player;



    public void Start()
    {
        //This gets the variables for the dialogue controller and player along with setting the node properly.  Note that the NPC behaviour should control what node is active on the npc's dialogue tree.
        dialogueCtrl = DialogueController.instance;
        i = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //interact activates the dialogue so long as the dialogue isn't active.
    public override void Interact()
    {
        base.Interact();
        if (!active)
        {
            activateDialogue();
        }
    }

    private void LateUpdate()
    {
        //This observes the current distance the player is from the speaker and forces the dialogue to deactivate should the player move too far.
        if (active)
        {
            float distancetoPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
            if (distancetoPlayer > maxDistFromPlayer)
            {
                forceDeactivateDialogue();
            }
        }
    }
    //Activate both activates the initial dialogue and it also swaps to  the further dialogue items in the tree.  If there's no more items, the dialogue ends.
    public void activateDialogue()
    {
        active = true;
        if(myDialogue[i] == null)
        {
            dialogueCtrl.DeactivateDialogue();
            active = false;
            i = 0;
            return;
        }
        if (myDialogue[i] != null)
        {
            dialogueCtrl.ActivateDialogue(myDialogue[i], this);
            i++;
        }
    }
    //ForceDeactivate can be called at any time to forcefully stop the dialogue tree.
    public void forceDeactivateDialogue()
    {
        dialogueCtrl.DeactivateDialogue();
        active = false;
        i = 0;
        return;
    }
}
