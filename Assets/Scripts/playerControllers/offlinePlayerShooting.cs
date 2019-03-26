﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum specialAbility {Absorb, Knife, Reflect, Dynamite, Icicle, Stealth, Tank}


public class offlinePlayerShooting : MonoBehaviour {

    public float dynamiteThrowForceForward = 50f;
    public float dynamiteThrowForceUpward = 50f;
    //These positions are where the bullets come from.
    public Transform GunPos1;
    public Transform GunPos2;

    //These base stats can be increased by leveling up or using certain weapons.
    public float timeToReload;
    public float ReloadTime;
    offlinePlayerMovement myMovement;
    player_Health myHealth;

    public float baseBulletDamage;

    playerStats playerstats;
    //These audio sources are for the gunshots
    public AudioSource gunShotAudio1;
    public AudioSource gunShotAudio2;

    //line renderers show where the bullet went
    public LineRenderer myGunShotRenderer1;
    public LineRenderer myGunShotRenderer2;

    //line renderer for shotgun blasts
    public LineRenderer[] shotGunRenderer = new LineRenderer[10];
    public Transform shotDirR;
    public Transform[] shotGunDir = new Transform[10];
    public Transform[] baseShotGunDir = new Transform[10];

    public Transform shotDirL;

    //These particle systems are the muzzleflash 
    public ParticleSystem bulletSystem1;
    public ParticleSystem bulletSystem2;
    

    public float range = 30f;

    bool rightHasAmmo = true;
    bool leftHasAmmo = true;

    //This is the UI for the current Ammo
    public Text maxRtxt;
    public Text curRtxt;
    public Text maxLtxt;
    public Text curLtxt;

    //These are the current Ammo counts for the two guns
    public int rightAmmoCount = 6;
    public int leftAmmoCount = 6;

    //These are the max ammo counts for the two guns.
    public int maxRAmmoCount = 6;
    public int maxLAmmoCount = 6;

    public bool canFire = true;

    bool hasntCanceled = false;
    bool right = false;
    bool isCurrentlyReloading = false;


    public bool Alive;

    Vector3 defVecRight;
    Vector3 defVecLeft;

    public GameObject LeftAmmoCounter;


    bool isDualWeapon = true;

    float spread;
    int shotsInSpread;
    weaponType myWeaponType;
    damageType myWeaponDamageType;
    EquipmentManager eqManage;
    float timeToReloadBullet = .2f;

    List<specialAbility> mySpAbilities = new List<specialAbility>();
    int currentSpAbility = 0;
    public Image spAbiImg;
    List<Sprite> spAbiImageSprite = new List<Sprite>();

    public specialAbility sp1;
    public Sprite sp1Img;
    public specialAbility sp2;
    public Sprite sp2Img;

    public Slider spAbiReload;

    float timeUntilUseAbility = 1f;
    float reloadAbilityTime = 1f;

    public GameObject magicKnife;


    public Animator myAnim;

    bool AbsorbActive = false;
    public GameObject absorbChecker;
    GameObject enemyToAbsorb;

    bool absorbConnected = false;

    public GameObject dynamite;
    public GameObject icicle;
    private void Start()
    {
        
        eqManage = EquipmentManager.instance;
        eqManage.onEquipmentChanged += updateWeapon;

        //get the playerstats to get damage boosts
        playerstats = GetComponent<playerStats>();

        //canfire is set false if reloading, dashing or elsewise.  For any reason the player can't fire, this is set false.
        canFire = true;

        //These Vectors are where the gunshots are shown with the linerenderers
        defVecRight = myGunShotRenderer1.GetPosition(1);
        defVecLeft = myGunShotRenderer2.GetPosition(1);

        //Set the current ammo equal to max ammo
        rightAmmoCount = maxRAmmoCount;
        leftAmmoCount = maxLAmmoCount;

        //set the first gun to fire as the left handed gun.  note that this will have to change if we include single wielded weapons instead of dual for all
        right = false;

        //hasn't canceled means that the player hasn't canceled the reloading sequence
        hasntCanceled = false;

        //currently reloading is true if reloading, elsewise false
        isCurrentlyReloading = false;

        //set the renderers for the lines as false in order to make them invisible for now.
        myGunShotRenderer1.enabled = false;
        
        myGunShotRenderer2.enabled = false;

        //The player has to be alive when start goes.
        Alive = true;

        //update the UI that shows current and max ammo.
        maxRtxt.text = maxRAmmoCount.ToString();
        maxLtxt.text = maxLAmmoCount.ToString();
        updateUI();
        
        int i = 0;
        while(i < 10)
        {
            
            shotGunRenderer[i].enabled = false;
            i++;
        }


        //add special ability, note to replace this later by a system that'll automatically have certain ones already collected loaded.
        mySpAbilities.Add(sp1);
        spAbiImageSprite.Add(sp1Img);
        mySpAbilities.Add(sp2);
        spAbiImageSprite.Add(sp2Img);
        updateSpUI();
        myMovement = GetComponent<offlinePlayerMovement>();
        myHealth = GetComponent<player_Health>();

    }

    public void updateWeapon(Equipment newItem, Equipment oldItem)
    {
        if (newItem as Weapon != null)
        {
            //Debug.Log(newItem);
            setWeapon((Weapon)newItem);
        }
    }

    public void setWeapon(Weapon myEquip)
    {
        
        if(myEquip == null)
        {
            return;
        }
        if (myEquip.equipSlot == EquipmentSlot.Weapon)
        {
            Weapon myWpn = (Weapon)myEquip;
            setStats(myWpn);
        }
    }

    public void setStats(Weapon wpn)
    {
        right = true;
        isDualWeapon = wpn.isDualWeapon;
        spread = wpn.spread;
        shotsInSpread = wpn.shotsInSpread;
        myWeaponType = wpn.myWeaponType;
        myWeaponDamageType = wpn.myType;
        baseBulletDamage = wpn.damageModifier;

        maxRAmmoCount = wpn.maxAmmo;
        maxLAmmoCount = wpn.maxAmmo;
        rightAmmoCount = wpn.maxAmmo;
        leftAmmoCount = wpn.maxAmmo;
        

        gunShotAudio1.clip = wpn.myAudio;
        gunShotAudio2.clip = wpn.myAudio;

        timeToReloadBullet = wpn.timeToReloadSingleShot;
        timeToReload = wpn.timeBetweenShots;

        maxRtxt.text = maxRAmmoCount.ToString();
        maxLtxt.text = maxLAmmoCount.ToString();
        reloadAllImmediately();
        updateUI();

        if (!isDualWeapon)
        {
            LeftAmmoCounter.GetComponent<Image>().enabled = false;
            Text[] myTxts = LeftAmmoCounter.GetComponentsInChildren<Text>();
            foreach(Text txt in myTxts)
            {
                txt.enabled = false;
            }
        }
        if (isDualWeapon)
        {
            LeftAmmoCounter.GetComponent<Image>().enabled = true;
            Text[] myTxts = LeftAmmoCounter.GetComponentsInChildren<Text>();
            foreach (Text txt in myTxts)
            {
                txt.enabled = true;
            }
        }
        int i = 0;
        while(i < 10)
        {
            //shotGunDir[i].RotateAround(gameObject.transform.position, Vector3.up, spread);
            shotGunDir[i].rotation = baseShotGunDir[i].rotation;
            //shotGunDir[i+1].RotateAround(gameObject.transform.position, Vector3.up, -spread);
            shotGunDir[i + 1].rotation = baseShotGunDir[i].rotation;
            i += 2;
        }
        i = 0;
        while(i < 10)
        {

            //shotGunDir[i].RotateAround(gameObject.transform.position, Vector3.up, spread);
            shotGunDir[i].Rotate(new Vector3(0, ((spread/2) * (i + 1)), 0));
            //shotGunDir[i+1].RotateAround(gameObject.transform.position, Vector3.up, -spread);
            shotGunDir[i + 1].Rotate(new Vector3(0, ((-spread/2) * (i+1)), 0));
            i+=2;
        }

        /*
        Debug.Log(wpn.name);
        Debug.Log(shotsInSpread);
        Debug.Log(spread);
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(absorbChecker.transform.position, .1f);
    }
    // Update is called once per frame
    void Update()
    {
        /*
        Debug.DrawRay(GunPos1.transform.position, shotDirR.forward * range, Color.red, 2f);
        Debug.DrawRay(GunPos2.transform.position, shotDirL.forward * range, Color.red, 2f);
        */
        if (Alive)
        {
            if (canFire)
            {
                if (Input.GetAxis("Fire1") > 0 && Time.time > ReloadTime)
                {
                    Fire();
                }
                if (Input.GetAxis("Reload") > 0 && !isCurrentlyReloading)
                {
                    Reload();
                }
                if (Input.GetAxisRaw("Fire1") > 0 && !hasntCanceled)
                {
                    hasntCanceled = true;
                }
                if(Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    if(Input.GetAxis("Mouse ScrollWheel") > 0.2f)
                    {
                        specialAbilityUp();
                        return;
                    }
                    if(Input.GetAxis("Mouse ScrollWheel") < 0.2f)
                    {
                        specialAbilityDown();
                        return;
                    }
                }
                if (Input.GetButton("SpecialAbility") && Time.time > timeUntilUseAbility)
                {
                    specialAbility(mySpAbilities[currentSpAbility]);
                }
            }
        }
        if (AbsorbActive)
        {
            if (absorbConnected == false)
            {
                absorbConnected = Physics.CheckSphere(absorbChecker.transform.position, .1f);

                Debug.Log("absorbConnected is: " + absorbConnected);
                if (absorbConnected == true)
                {
                    if(enemyToAbsorb == null) {
                    RaycastHit hit;
                    Debug.DrawRay(absorbChecker.transform.position, absorbChecker.transform.forward, Color.red, 2f);
                    Ray newRay = new Ray(absorbChecker.transform.position, absorbChecker.transform.forward);
                        if (Physics.Raycast(newRay, out hit, 2f))
                        {
                            if (hit.collider.tag == "Enemy")
                            {
                                Debug.Log("Collided with enemy");
                                enemyToAbsorb = hit.collider.gameObject;
                            }
                        }
                        Debug.Log("enemy To Absorb is: " + enemyToAbsorb);
                    }
                }
            }
        }
    }

    public void updateUI()
    {
        curRtxt.text = rightAmmoCount.ToString();
        curLtxt.text = leftAmmoCount.ToString();
    }
    #region ability Ending coroutines
    IEnumerator endKnife()
    {
        myMovement.startAbsorbDash(1);
        yield return new WaitForSeconds(reloadAbilityTime);
        myAnim.SetBool("Knife", false);
        canFire = true;
        magicKnife.SetActive(false);

    }
    IEnumerator endAbsorb()
    {
        enemyToAbsorb = null;
        absorbConnected = false;
        myMovement.startAbsorbDash(1.5f);
        myHealth.isDashing = true;
        yield return new WaitForSeconds(reloadAbilityTime);
        AbsorbActive = true;
        yield return new WaitForSeconds(.2f);

        if (absorbConnected == false)
        {
            canFire = true;
            myAnim.SetBool("Absorb", false);
            AbsorbActive = false;
        }
        if (absorbConnected)
        {
            AbsorbConnected();
        }
    }
    void AbsorbConnected()
    {
        StartCoroutine("endAbsorbConnected");
    }
    IEnumerator endAbsorbConnected()
    {
        yield return new WaitForSeconds(.5f);
        if (enemyToAbsorb != null)
        {
            bool kill;
            enemyToAbsorb.GetComponent<enemyHealth>().takeDamage(10, damageType.magical);
            kill = enemyToAbsorb.GetComponent<enemyHealth>().isDead();
            if (kill)
            {
                Inventory.instance.giveExp(enemyToAbsorb.GetComponent<enemyHealth>().ExpToGive);
            }
        }
        canFire = true;
        myAnim.SetBool("Absorb", false);
        AbsorbActive = false;
    }
    IEnumerator endDynamite()
    {
        canFire = false;
        throwDynamite();
        yield return new WaitForSeconds(reloadAbilityTime);
        canFire = true;
    }
    void throwDynamite()
    {
        GameObject thrower;
        thrower = Instantiate(dynamite, absorbChecker.transform.position, Quaternion.Euler(0, 0, 0));
        Rigidbody throwRB;
        throwRB = thrower.GetComponent<Rigidbody>();
        throwRB.AddForce(transform.forward * dynamiteThrowForceForward);
        throwRB.AddForce(transform.up * dynamiteThrowForceUpward);
    }
    IEnumerator endIcicle()
    {
        canFire = false;
        Instantiate(icicle, absorbChecker.transform.position, Quaternion.Euler(0, 0, 0));
        icicle.transform.rotation = gameObject.transform.rotation;
        yield return new WaitForSeconds(reloadAbilityTime);
        canFire = true;

    }
    IEnumerator endReflect()
    {
        yield return new WaitForSeconds(reloadAbilityTime);

    }
    IEnumerator endStealth()
    {
        yield return new WaitForSeconds(reloadAbilityTime);

    }
    IEnumerator endTank()
    {
        yield return new WaitForSeconds(reloadAbilityTime);

    }
    #endregion

    void specialAbility(specialAbility myabi)
    {
        if(myabi == global::specialAbility.Absorb)
        {
            Debug.Log("Absorbing");
            myAnim.SetBool("Absorb", true);
            reloadAbilityTime = 1f;
            canFire = false;
            StartCoroutine("endAbsorb");
        }
        if (myabi == global::specialAbility.Dynamite)
        {
            Debug.Log("kaboom");
            StartCoroutine("endDynamite");
            reloadAbilityTime = 1f;
        }
        if (myabi == global::specialAbility.Icicle)
        {
            Debug.Log("Icy");
            reloadAbilityTime = 1f;
        }
        if (myabi == global::specialAbility.Knife)
        {
            Debug.Log("Shank");
            canFire = false;
            myAnim.SetBool("Knife", true);
            magicKnife.SetActive(true);
            reloadAbilityTime = 2f;
            StartCoroutine("endKnife");
        }
        if (myabi == global::specialAbility.Reflect)
        {
            Debug.Log("Reflecting");
            reloadAbilityTime = 1f;
        }
        if (myabi == global::specialAbility.Stealth)
        {
            Debug.Log("Stealthing");
            reloadAbilityTime = 1f;
        }
        if (myabi == global::specialAbility.Tank)
        {
            Debug.Log("Tank Time");
            reloadAbilityTime = 1f;
        }
        
        timeUntilUseAbility = Time.time + reloadAbilityTime;
        SpecialAbilityReloadUIUpdate();
    }
    void SpecialAbilityReloadUIUpdate()
    {
        spAbiReload.maxValue = timeUntilUseAbility;
        spAbiReload.minValue = Time.time;
    }
    void specialAbilityUp()
    {
        currentSpAbility++;
        if(currentSpAbility > mySpAbilities.Count)
        {
            currentSpAbility = 0;
        }
        Debug.Log(mySpAbilities[currentSpAbility]);
        updateSpUI();
    }
    void specialAbilityDown()
    {
        currentSpAbility--;
        if(currentSpAbility < 0)
        {
            currentSpAbility = mySpAbilities.Count - 1;
        }
        Debug.Log(mySpAbilities[currentSpAbility]);
        updateSpUI();
    }
    public void addSpAbility(SpecialAbilityObject abiToAdd)
    {
        if (mySpAbilities.Contains(abiToAdd.myAbility))
        {
            return;
        }
        mySpAbilities.Add(abiToAdd.myAbility);
        spAbiImageSprite.Add(abiToAdd.AbilityImage);
    }
    void updateSpUI()
    {
        spAbiImg.sprite = spAbiImageSprite[currentSpAbility];

    }

    void Reload()
    {
        StartCoroutine("ReloadCoroutine");
    }
    IEnumerator ReloadCoroutine()
    {
        isCurrentlyReloading = true;
        hasntCanceled = false;
        yield return new WaitForSeconds(timeToReloadBullet);
        while (!hasntCanceled)
        {
            if (rightAmmoCount < maxRAmmoCount)
            {
                
                rightAmmoCount++;
                updateUI();
            }
            if (isDualWeapon)
            {
                if (leftAmmoCount < maxLAmmoCount)
                {

                    leftAmmoCount++;
                    updateUI();
                }
            }
            if (rightAmmoCount >= maxRAmmoCount && leftAmmoCount >= maxLAmmoCount)
            {
                hasntCanceled = true;
            }
            yield return new WaitForSeconds(timeToReloadBullet);

        }
        if (rightAmmoCount > 0)
        {
            rightHasAmmo = true;
        }
        if (leftAmmoCount > 0)
        {
            leftHasAmmo = true;
        }
        isCurrentlyReloading = false;
        updateUI();
    }

    public void reloadAllImmediately()
    {
        while(rightAmmoCount < maxRAmmoCount)
        {
            
            rightAmmoCount++;
        }
        while(leftAmmoCount < maxLAmmoCount)
        {
            
            leftAmmoCount++;
        }
        if(rightAmmoCount > maxRAmmoCount)
        {
            rightAmmoCount = maxRAmmoCount;
        }
        if(leftAmmoCount > maxLAmmoCount)
        {
            leftAmmoCount = maxLAmmoCount;
        }
        rightHasAmmo = true;
        leftHasAmmo = true;
        updateUI();

        return;
    }


    void Fire()
    {
        
        RaycastHit hit = new RaycastHit();
        
        bool canFire = true;
        if(myWeaponType == weaponType.Shotgun)
        {
            if(canFire && rightHasAmmo)
            {
                Debug.Log("Shotgun Blast");
                gunShotAudio1.PlayOneShot(gunShotAudio1.clip);
                bulletSystem1.Play();
                canFire = false;
                rightAmmoCount--;

                Ray centerRay = new Ray(GunPos1.position, shotDirR.position * range);
                myGunShotRenderer1.enabled = true;
                
                //always have one shot go straight
                float damage = baseBulletDamage + playerstats.damage.GetValue();
                if (Physics.Raycast(centerRay, out hit, range))
                {
                    Vector3 distance = new Vector3(0, 0, Vector3.Distance(hit.collider.gameObject.transform.position, GunPos1.position));
                    myGunShotRenderer1.SetPosition(1, distance);
                    m_Health hitHp = hit.collider.gameObject.GetComponent<m_Health>();
                    //applies damage to object
                    if (hitHp != null)
                    {
                        hitHp.takeDamage(damage, myWeaponDamageType);
                    }
                }
                //include multiple hits real soon
                for(int i = 0; i < shotsInSpread; i++)
                {
                    Ray ray = new Ray(GunPos1.position, shotGunDir[i].forward * range);
                    shotGunRenderer[i].enabled = true;
                    Debug.DrawRay(GunPos1.position, shotGunDir[i].forward * range, Color.red, 2f);
                    //shotGunRenderer[i].SetPosition(1, shotGunDir[i].forward * range);
                    if (Physics.Raycast(ray, out hit, range))
                    {
                        Debug.Log("Hit");
                       // Vector3 distance = new Vector3(0,0, Vector3.Distance(hit.collider.gameObject.transform.position, GunPos1.position));
                        //myGunShotRenderer1.SetPosition(1, distance);
                        m_Health hitHp = hit.collider.gameObject.GetComponent<m_Health>();
                        //applies damage to object
                        if (hitHp != null)
                        {
                            hitHp.takeDamage(damage, myWeaponDamageType);
                        }
                    }
                }

                

                if (rightAmmoCount <= 0)
                {
                    rightHasAmmo = false;
                }
                updateUI();
            }

            
        }
        if (myWeaponType == weaponType.dualRevolvers || myWeaponType == weaponType.Rifle)
        {
            //Debug.Log("Single Shot");
            if (right && canFire && rightHasAmmo)
            {
                gunShotAudio1.PlayOneShot(gunShotAudio1.clip);
                Ray myRay = new Ray(GunPos1.position, shotDirR.forward * range);

                bulletSystem1.Play();
                canFire = false;
                rightAmmoCount--;

                myGunShotRenderer1.enabled = true;
                //Debug.Log(rightAmmoCount);
                if (rightAmmoCount <= 0)
                {
                    rightHasAmmo = false;
                }
                if (Physics.Raycast(myRay, out hit, range))
                {
                    float damage = baseBulletDamage + playerstats.damage.GetValue();
                    Vector3 distance = new Vector3(0, 0, Vector3.Distance(hit.collider.gameObject.transform.position, GunPos1.position));
                    myGunShotRenderer1.SetPosition(1, distance);
                    m_Health hitHp = hit.collider.gameObject.GetComponent<enemyHealth>();
                    //applies damage to object
                    if (hitHp != null)
                    {
                        hitHp.takeDamage(damage, myWeaponDamageType);
                    }


                }
                updateUI();



            }



            if (isDualWeapon)
            {
                if (!right && canFire && leftHasAmmo)
                {

                    gunShotAudio2.PlayOneShot(gunShotAudio2.clip);
                    Ray myRay = new Ray(GunPos2.position, shotDirL.forward * range);
                    bulletSystem2.Play();
                    canFire = false;
                    leftAmmoCount--;

                    myGunShotRenderer2.enabled = true;
                    //Debug.Log(leftAmmoCount);
                    if (leftAmmoCount <= 0)
                    {
                        leftHasAmmo = false;
                    }

                    if (Physics.Raycast(myRay, out hit, range))
                    {
                        float damage = baseBulletDamage + playerstats.damage.GetValue();
                        Vector3 distance = new Vector3(0, 0, Vector3.Distance(hit.collider.gameObject.transform.position, GunPos2.position));
                        myGunShotRenderer2.SetPosition(1, distance);

                        m_Health hitHp = hit.collider.gameObject.GetComponent<m_Health>();
                        //applies damage to object
                        if (hitHp != null)
                        {
                            hitHp.takeDamage(damage, myWeaponDamageType);
                        }
                    }
                    updateUI();
                }
            }
        }



        ReloadTime = Time.time + timeToReload;
        right = !right;
        if (!isDualWeapon)
        {
            right = true;
        }
        StartCoroutine("endGunShotRender");
    }

    IEnumerator endGunShotRender()
    {
        yield return new WaitForSeconds(.05f);
        myGunShotRenderer2.SetPosition(1,defVecLeft);
        myGunShotRenderer1.SetPosition(1, defVecRight);
        
        int i = 0;
        while(i < shotsInSpread)
        {
            shotGunRenderer[i].enabled = false;
            i++;
        }
        
        myGunShotRenderer1.enabled = false;
        myGunShotRenderer2.enabled = false;
    }
}
