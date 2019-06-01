using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLadder : InteractablePickup
{
    //The player is the gameobject that the player controls, and the pMove is the movement script this script needs to communicate to.
    GameObject player;
    offlinePlayerMovement pMove;
    //bool climbing results in the script asking if we need to set stop or start climbing in the player controller.
    bool climbing = false;
    //These transforms are the transforms of the ladder, resulting in transforms that set where you are baseline, the max height and min height, and the positions you go to after reaching max or min height.
    public Transform upperLimit;
    public Transform UpperPlayerSet;
    public Transform basePositionY;
    public Transform lowerLimit;
    public Transform LowerPlayerSet;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            pMove = player.GetComponent<offlinePlayerMovement>();
        }
    }
    public override void Interact()
    {
        base.Interact();
        Climb();
    }
    private void LateUpdate()
    {
        //This asks if the player is currently climbing, and if so, did it go above the max height or below the min height.  At this point the player stops climbing and then is snapped to the upper or lower position.
        if (climbing)
        {
            if(player.transform.position.y < lowerLimit.position.y)
            {
                climbing = false;
                pMove.StopClimbing();
                player.transform.position = LowerPlayerSet.position;
            }
            if(player.transform.position.y > upperLimit.position.y)
            {
                climbing = false;
                pMove.StopClimbing();
                player.transform.position = UpperPlayerSet.position;
            }
        }
    }

    void Climb()
    {
        if (!climbing)
        {
            climbing = true;
            if(player == null || pMove == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    pMove = player.GetComponent<offlinePlayerMovement>();
                }
            }
            player.transform.position = basePositionY.position;
            pMove.StartClimbing();
        }
    }
}
