using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class offlinePlayerMovement : MonoBehaviour
{
    UIController UI;
    //Inventory Screen is the canvas that holds the inventory UI
    public GameObject InventoryScreen;
    //CamTarget allows us to target specific areas of the screen or swap targets between the player
    public GameObject CamTarget;
    //animup is when the inventory screen or pause screen is active
    bool animUp = false;
    //floormask is the int of the floor, this is used to rotate the player. camraylength just makes sure that the raycast is far enough to hit the floor
    public int floormask;
    public float camRayLength = 1000;
    public float acceleration;
    //player rigidbody is the physics object we are controlling to affect the player's movement.
    Rigidbody playerRigidbody;
    //speed is an averaged speed that uses time.deltatime to be as smooth as possible
    float baseSpeed = 350;
    float speed;
    //collider is used to 
    Collider myCollider;

    public bool Alive;
    
    offlinePlayerShooting myShooting;
    player_Health myHealth;

    public Animator myAnim;
    //dash reload and timetoreload dash are the two modifiers that make the dash usable and balanced.  They result in the dash not being used constantly
    float dashReload = 1f;
    float timetoReloadDash = 5f;
    //dash multiplier is how much faster than normal you move when dashing, currently dashing is a bool that's true when dashing, false when otherwise.  This is used to plan for not being able to reload or dash a second time when dashing.
    float dashMultiplier = 2f;
    bool currentlyDashing = false;
    //mylight will be a light that is held by the player eventually, this would most likely be a lantern that affects stealth skills.  With BaseIntensity being the base intensity of the lantern itself.
    public Light mylight;
    public float baseintensity = 5;

    //mycam and thiscam are the camera and the cam follow script that we use in Start to set the camera's target and offset
    Camera myCam;
    camFollowTarget thisCam;

    //climbing and climbing speed
    bool climbing = false;
    public float climbingSpeed = 2f;

    //checks if the player is grounded, and if so then gravity is changed to be effective.
    public Transform groundCheck;
    bool grounded;
    public float groundCheckRadius = .5f;
    public LayerMask floor;
    public float fallSpeed = 5f;
    float currentFallSpeed;

    public bool canMove = true;

    public LayerMask interactableLayer;
    playerStats myStats;
    devConsole myconsole;
    // Use this for initialization
    void Start()
    {
        UI = UIController.instance;
        myconsole = devConsole.instance;
        myHealth = GetComponent<player_Health>();
        myStats = GetComponent<playerStats>();
        
        speed = baseSpeed + (myStats.speed.GetValue() / 10);
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
        Camera[] Cameras = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera Cam in Cameras)
        {
            //Debug.Log("Finding Cameras");
            camFollowTarget temp = Cam.GetComponent<camFollowTarget>();
            if (temp != null)
            {
                //Debug.Log("Cam Follow Target has been found");
                thisCam = Cam.GetComponent<camFollowTarget>();

                bool hasTarget = thisCam.returnTarget();
                if (!hasTarget)
                {
                    //Debug.Log("Target set");
                    thisCam.target = CamTarget.transform;
                    myCam = Cam;
                    break;
                }
            }
            
        }



        InventoryScreen.SetActive(false);
    }
    public void respawn()
    {
        return;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
    public void setSpeed()
    {
        if (myStats != null)
        {
            speed = baseSpeed + (myStats.speed.GetValue() / 10);
        }
        else
        {
            Debug.LogWarning("Player Stats not found");
            myStats = GetComponent<playerStats>();
            setSpeed();
        }
        //Debug.Log(speed);
        return;
    }
    public void startAbsorbDash(float amt)
    {
        absorbDash(amt);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Console"))
        {
            myconsole.onCommand();

        }

        if (PlayerPrefs.GetFloat("devConsoleUp") == 0)
        {
            if (climbing)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    transform.Translate((Vector3.up * Time.deltaTime) * climbingSpeed);
                }
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    transform.Translate((Vector3.down * Time.deltaTime) * (climbingSpeed * 2));
                }
            }
            //Anim up is false if the menu isn't open.
            if (!animUp)
            {
                if (!climbing)
                {
                    if (canMove)
                    {
                        //Check if the player is grounded, if he isn't, then use gravity, elsewise don't.
                        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, floormask);
                        //Debug.Log(grounded);

                        if (grounded)
                        {
                            currentFallSpeed = 0;
                        }
                        if (!grounded)
                        {
                            currentFallSpeed = fallSpeed;
                        }
                        float MovX;
                        float movZ;
                        MovX = Input.GetAxisRaw("Horizontal");
                        movZ = Input.GetAxisRaw("Vertical");
                        if (currentlyDashing == false)
                        {
                            moveMe(MovX, movZ, currentFallSpeed);

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
                        if (Input.GetButtonDown("QuickUse"))
                        {
                            QuickUse();
                        }
                    }

                }
            }
            if (animUp)
            {
                moveMe(0, 0, currentFallSpeed);
            }
            //pull up the inventory screen if tab is pressed, we could also have it be I for inventory
            if (Input.GetButtonDown("Tab"))
            {
                InventoryControl();

            }
            //Interact will always be the spacebar
            if (Input.GetButtonDown("Interact"))
            {
                Interact();
            }
            //lightchange will be the F key
            if (Input.GetButtonDown("LightChange"))
            {
                swapLight();
            }
        }
    }
    //swap Light turns the light on and off upon button pressing.
    void swapLight()
    {
        mylight.enabled = !mylight.enabled;
    }
    //Interact will call the interactable function if you're over an interactable object.
    void Interact()
    {
        //sends out a ray from the camera, and sends it to the mouse position
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, camRayLength, interactableLayer,  QueryTriggerInteraction.Ignore))
        {
            //If you hit something, check to see if it's interactable
            InteractablePickup interactable = hit.collider.GetComponent<InteractablePickup>();
            if(interactable != null)
            {
                if (interactable.isInRadius)
                {
                    //If it's interactable and you're within the radius of the object, call the interaction funciton on it.
                    interactable.Interact();
                }
            }
        }
    }

    void QuickUse()
    {
        if(myHealth.health < myHealth.maxhp) { 
        Debug.Log("Using Quick Use");
        bool potionUsed = false;
        for(int x = 0; x < 20; x++) {
            Item i = Inventory.instance.getItem(x);
                if (potionUsed == false)
                {
                    if (i != null)
                    {
                        Debug.Log("Item is: " + i.name);
                        if (i.isConsumable == true)
                        {
                            Debug.Log("Item is consumable");

                            Consumable itemToUse = (Consumable)i;
                            if (itemToUse.isPotion)
                            {
                                Debug.Log("Item is a potion");
                                itemToUse.Use();
                                potionUsed = true;
                            }
                        }
                    }
                }
            }
        }
    }

    //inventory control controls whether the inventory screen is active or not.
    void InventoryControl()
    {
            if (animUp)
            {
                //InventoryScreen.SetActive(false);
                UI.deactivateUI();
                PlayerPrefs.SetFloat("InventoryUp", 0);
                myShooting.canFire = true;
                animUp = false;
                return;
            }
            if (!animUp)
            {
                UI.activateUI("InventoryUI");
                InventoryScreen.SetActive(true);
                PlayerPrefs.SetFloat("InventoryUp", 1);
                myShooting.canFire = false;
                animUp = true;
                return;
            }
        
    }
    //Dash controls the dashing ability on the player.
    void absorbDash(float dashMult)
    {
        currentlyDashing = true;
        //then apply physics to show the dash.
        Vector3 mov = transform.forward * (dashMultiplier * dashMult);
        playerRigidbody.velocity = ((mov * speed) * Time.deltaTime);
        //We then call the endDash Coroutine, which waits, then resets everything.
        StartCoroutine("EndDash");
    }
    void Dash(float movX, float movZ)
    {
        //set the dashing bools for both the controller and the animator true.
        currentlyDashing = true;
        myAnim.SetBool("Dashing", true);
        
        //set the player as undamageable during the dash
        myHealth.isDashing = true;
        //reload all ammo in the guns.
        myShooting.reloadAllImmediately();
        //then apply physics to show the dash.
        Vector3 mov = new Vector3(movX * dashMultiplier, 0, movZ * dashMultiplier);
        playerRigidbody.velocity = ((mov * speed) * Time.deltaTime);
        //We then call the endDash Coroutine, which waits, then resets everything.
        StartCoroutine("EndDash");
    }
    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(.1f);
        myHealth.isDashing = false;
        myAnim.SetBool("Dashing", false);
        playerRigidbody.velocity = Vector3.zero;
        currentlyDashing = false;
    }
    
    //Move me takes information from the axes, and then applies them to both the animator and the player controller.
    void moveMe(float movX, float movZ, float fallSpeed)
    {

        Vector3 movement = new Vector3(movX, fallSpeed,movZ);
        float myspd = speed;
        if(Input.GetAxis("Sprint") > .1f)
        {
            myspd = speed * 1.5f;
            if (movX != 0 && movZ != 0) {
                myShooting.canFire = false;
            }
        }
        if(Input.GetAxis("Sprint") < .1f)
        {
            myspd = speed;
            myShooting.canFire = true;
        }
        movement = (movement.normalized * myspd) * Time.smoothDeltaTime;
        movement.y = fallSpeed;
        playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, movement, acceleration);
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
            //It also controls the rotation of the player in relation of where the mouse is.
            Ray camRay = myCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floormask, QueryTriggerInteraction.Ignore ))
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
       if(thisCam == null)
        {
            //Debug.Log("Camera not available");
        }
        thisCam.getNewGrid(grid, grid.offset);
    }
    //Start and Stop climbing effect how the player gets on ladders
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

    public void resetCamPos()
    {
        thisCam.resetPosition();
    }
    
}
