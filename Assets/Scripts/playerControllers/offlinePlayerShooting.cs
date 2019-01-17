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
    public LineRenderer[] shotGunRenderer;

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


    bool isDualWeapon;
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


        

        setWeapon();
    }

    public void updateWeapon(Equipment newItem, Equipment oldItem)
    {
        Debug.Log("Setting weapon");
        setWeapon();
    }

    void setWeapon()
    {
        Equipment myEquip = eqManage.getEquipment(2);
        if(myEquip == null)
        {
            Debug.Log("Equipment is null");
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

        gunShotAudio1 = wpn.myAudio;
        gunShotAudio2 = wpn.myAudio;

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
        
        if (right && canFire && rightHasAmmo)
        {
            gunShotAudio1.PlayOneShot(gunShotAudio1.clip);
            Ray myRay = new Ray(transform.position, transform.forward);
           
            myRay = new Ray(GunPos1.position, transform.forward * range);
            
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
                    float damage = baseBulletDamage + playerstats.damage.GetValue() + ;
                    Vector3 distance = new Vector3(0, 0, Vector3.Distance(hit.collider.gameObject.transform.position, transform.position));
                    myGunShotRenderer1.SetPosition(1, distance);
                    m_Health hitHp = hit.collider.gameObject.GetComponent<m_Health>();
                    //applies damage to object
                    if (hitHp != null)
                    {
                        hitHp.takeDamage(damage, damageType.physical);
                    }

                
            }
            updateUI();
            


        }




        if (!right && canFire && leftHasAmmo)
        {
        
            gunShotAudio2.PlayOneShot(gunShotAudio2.clip);
            Ray myRay = new Ray(GunPos2.position, transform.forward * range);
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
                Vector3 distance = new Vector3(0, 0, Vector3.Distance(hit.collider.gameObject.transform.position, transform.position));
                myGunShotRenderer2.SetPosition(1, distance);

                m_Health hitHp = hit.collider.gameObject.GetComponent<m_Health>();
                //applies damage to object
                if (hitHp != null)
                {
                    hitHp.takeDamage(damage, damageType.physical);
                }
            }
            updateUI();
        }



        ReloadTime = Time.time + timeToReload;
        right = !right;
        StartCoroutine("endGunShotRender");
    }

    IEnumerator endGunShotRender()
    {
        yield return new WaitForSeconds(.02f);
        myGunShotRenderer2.SetPosition(1,defVecLeft);
        myGunShotRenderer1.SetPosition(1, defVecRight);
        myGunShotRenderer1.enabled = false;
        myGunShotRenderer2.enabled = false;
    }
}
