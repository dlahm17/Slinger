using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum specialAbility {Absorb, Dynamite, Icicle }


public class offlinePlayerShooting : MonoBehaviour {
    //These positions are where the bullets come from.
    public Transform GunPos1;
    public Transform GunPos2;

    //These base stats can be increased by leveling up or using certain weapons.
    public float timeToReload;
    public float ReloadTime;


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

    public Transform shotDirL;

    //These particle systems are the muzzleflash 
    public ParticleSystem bulletSystem1;
    public ParticleSystem bulletSystem2;
    

    public float range = 10f;

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
    bool isSingleShot;
    bool isSpread;

    float spread;
    int shotsInSpread;
    weaponType myWeaponType;
    damageType myWeaponDamageType;
    EquipmentManager eqManage;
    float timeToReloadBullet = .2f;

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
        

        setWeapon((Weapon)eqManage.getWeapon());
    }

    public void updateWeapon(Equipment newItem, Equipment oldItem)
    {
        if ((Weapon)newItem)
        {
            Debug.Log(newItem);
        }
        setWeapon((Weapon)newItem);
    }

    void setWeapon(Weapon myEquip)
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
        isSingleShot = wpn.isSingleShot;
        isSpread = wpn.isSpread;
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
            shotGunDir[i].Rotate(new Vector3(0, ((spread/2) * (i + 1)), 0));
            //shotGunDir[i+1].RotateAround(gameObject.transform.position, Vector3.up, -spread);
            shotGunDir[i + 1].Rotate(new Vector3(0, ((-spread/2) * (i+1)), 0));
            i+=2;
        }

        Debug.Log(wpn.name);
        Debug.Log(shotsInSpread);
        Debug.Log(spread);

    }


    // Update is called once per frame
    void Update()
    {
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
            }
        }
    }

    public void updateUI()
    {
        curRtxt.text = rightAmmoCount.ToString();
        curLtxt.text = leftAmmoCount.ToString();
    }

    void specialAbility(specialAbility myabi)
    {

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
                Ray[] myRays = new Ray[shotsInSpread];
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
            Debug.Log("Single Shot");
            if (right && canFire && rightHasAmmo)
            {
                gunShotAudio1.PlayOneShot(gunShotAudio1.clip);
                Ray myRay = new Ray(GunPos1.position, shotDirR.position * range);

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
                    m_Health hitHp = hit.collider.gameObject.GetComponent<m_Health>();
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
                    Ray myRay = new Ray(GunPos2.position, shotDirL.position * range);
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
