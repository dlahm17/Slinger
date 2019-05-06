using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor_Directional : InteractablePickup
{
    //proper side and not right side are two transforms that go on either side of the door.
    public Transform properSide;
    public Transform notRightSide;
    public GameObject player;

    //These show confirmation or negative answers to opening.
    public NPCDialogueNode positiveNode;
    public NPCDialogueNode negativeNode;
    public AudioSource negativeAudio;

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Interact()
    {
        base.Interact();
        //if the player is closer to the transform on the right side than the wrong side, it'll open the door, otherwise it'll say a dialogue that the door doesn't open from this side.s
        if(Vector3.Distance(properSide.position, player.transform.position) < Vector3.Distance(notRightSide.position, player.transform.position))
        {
            //note when making a door make sure that the two transforms are a decent distance apart, but not so far that if the player is right on the door, they'll be closer to the other transform.
            open();
            DialogueController.instance.ActivateDialogue(positiveNode.myDialogue[0], positiveNode);
        }
        else
        {
            DialogueController.instance.ActivateDialogue(negativeNode.myDialogue[0], negativeNode);
            negativeAudio.PlayOneShot(negativeAudio.clip, 1);
        }
    }

    public void open()
    {
        Debug.Log("Opening");
        Animator myAnim = GetComponent<Animator>();
        myAnim.SetBool("Open", true);
    }
}
