using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class offlinePlayerMovement : MonoBehaviour
{
    //Inventory Screen is the canvas that holds the inventory UI
    public GameObject InventoryScreen;
    //CamTarget allows us to target specific areas of the screen or swap targets between the player
    public GameObject CamTarget;
    //animup is when the inventory screen or pause screen is active
    bool animUp = false;
    //floormask is the int of the floor, this is used to rotate the player. camraylength just makes sure that the raycast is far enough to hit the floor
    public int floormask;
    public float camRayLength = 1000;
    //player rigidbody is the physics object we are controlling to affect the player's movement.
    Rigidbody playerRigidbody;
    //speed is an averaged speed that uses time.deltatime to be as smooth as possible
    public float speed;
    //collider is used to 
    Collider myCollider;

    public bool Alive;
    
    offlinePlayerShooting myShooting;

    public Animator myAnim;
    //dash reload and timetoreload dash are the two modifiers that make the dash usable and balanced.  They result in the dash not being used constantly
    float dashReload = 1f;
    float timetoReloadDash = 5f;
    //dash multiplier is how much faster than normal you move when dashing, currently dashing is a bool that's true when dashing, false when otherwise.  This is used to plan for not being able to reload or dash a second time when dashing.
    float dashMultiplier = 2f;
    bool currentlyDashing = false;
    //mylight will be a light that is held by the player eventually, this would most likely be a lantern that affects stealth skills.  With BaseIntensity being the base intensity of the lantern itself.
    Light mylight;
    public float baseintensity = 5;

    //mycam and thiscam are the camera and the cam follow script that we use in Start to set the camera's target and offset
    Camera myCam;
    camFollowTarget thisCam;

    //climbing and climbing speed
    bool climbing = false;
    public float climbingSpeed = 2f;

    // Use this for initialization
    void Start()
    {
        myCam = Camera.main;
        if(InventoryScreen != null)
        {
            animUp = false;
        }
        myShooting = GetComponent<offlinePlayerShooting>();
        myCollider = GetComponent<Collider>();
        Alive = true;
        floormask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
        mylight = GetComponent<Light>();
        Camera[] Cameras = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera Cam in Cameras)
        {
            thisCam = Cam.GetComponent<camFollowTarget>();
            bool hasTarget = thisCam.returnTarget();
            if (!hasTarget)
            {
                thisCam.target = CamTarget.transform;
                myCam = Cam;
                break;
            }
        }



        InventoryScreen.SetActive(false);
    }
    public void respawn()
    {
        return;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("8"))
        {
            Debug.Log("took damage");
            player_Health myhp = GetComponent<player_Health>();
            myhp.takeDamage(1, damageType.physical);
        }
        if (climbing)
        {
            if(Input.GetAxisRaw("Vertical") > 0)
            {
                transform.Translate((Vector3.up * Time.deltaTime) * climbingSpeed);
            }
            if(Input.GetAxisRaw("Vertical") < 0)
            {
                transform.Translate((Vector3.down * Time.deltaTime) * climbingSpeed);
            }
        }
            if (!animUp)
            {
            if (!climbing)
            {
                float MovX;
                float movZ;
                MovX = Input.GetAxisRaw("Horizontal");
                movZ = Input.GetAxisRaw("Vertical");
                if (currentlyDashing == false)
                {
                    moveMe(MovX, movZ);
                    //mylight.intensity = (Mathf.Abs(MovX) + Mathf.Abs(movZ)) * baseintensity;
                }
                if (!Alive && myCollider.enabled)
                {
                    myCollider.enabled = false;
                }
                if (Alive && !myCollider.enabled)
                {
                    myCollider.enabled = true;
                }

                if (Input.GetAxis("Fire2") > .5f && Time.time > dashReload)
                {
                    Dash(MovX, movZ);
                    dashReload = Time.time + timetoReloadDash;
                }
            }
            }
            if (animUp)
            {
                moveMe(0, 0);
            }
            if (Input.GetButtonDown("Tab"))
            {
                InventoryControl();

            }
            if (Input.GetButtonDown("Interact"))
            {
                Interact();
            }
        
    }

    void Interact()
    {
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, camRayLength))
        {
            InteractablePickup interactable = hit.collider.GetComponent<InteractablePickup>();
            if(interactable != null)
            {
                if (interactable.isInRadius)
                {
                    interactable.Interact();
                }
            }
        }
    }

    void InventoryControl()
    {
        
            if (animUp)
            {
                InventoryScreen.SetActive(false);
                myShooting.canFire = true;
                animUp = false;
                return;
            }
            if (!animUp)
            {
                InventoryScreen.SetActive(true);
                myShooting.canFire = false;
                
                animUp = true;
                return;
            }
        
    }
    void Dash(float movX, float movZ)
    {
        currentlyDashing = true;
        myAnim.SetBool("Dashing", true);
        Debug.Log("Dashing");
        myShooting.reloadAllImmediately();
        //mylight.intensity = baseintensity * (Mathf.Abs(movX) + Mathf.Abs(movZ)) * dashMultiplier;
        Vector3 mov = new Vector3(movX * dashMultiplier, 0, movZ * dashMultiplier);
        playerRigidbody.velocity = ((mov * speed) * Time.deltaTime);
        StartCoroutine("EndDash");
    }
    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(.25f);
        myAnim.SetBool("Dashing", false);
        playerRigidbody.velocity = Vector3.zero;
        currentlyDashing = false;
    }

    void moveMe(float movX, float movZ)
    {

        Vector3 movement = new Vector3(movX, 0, movZ);
        movement = (movement.normalized * speed) * Time.smoothDeltaTime;
        playerRigidbody.velocity = movement;
        if(movX != 0 || movZ != 0)
        {
            myAnim.SetFloat("Speed", 1);
        }
        else if(movX == 0 && movZ == 0)
        {
            myAnim.SetFloat("Speed", 0);
        }
        if (!animUp)
        {
            Ray camRay = myCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floormask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotation);
            }
        }

    }
    public void changeGridAnim(EnemyGridController grid)
    {
        thisCam.getNewGrid(grid);
    }

    public void StartClimbing()
    {
        climbing = true;
        playerRigidbody.useGravity = false;
        playerRigidbody.isKinematic = true;
        playerRigidbody.velocity = Vector3.zero;
    }
    public void StopClimbing()
    {
        climbing = false;

        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.useGravity = true;
        playerRigidbody.isKinematic = false;
    }
    
}
