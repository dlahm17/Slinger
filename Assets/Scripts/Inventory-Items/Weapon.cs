using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType { dualRevolvers, Rifle, Shotgun}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
[SerializeField]
public class Weapon : Equipment
{
    EquipmentSlot mySlot = EquipmentSlot.Weapon;
    //weapon type determines spread, and weapon base.  it also determines player animations
    public weaponType myWeaponType;
    //damage type is the type of damage that the gun has an affinity for.  for example, poison type would deal some physical damage, but also deal poison damage and apply the status effect to the target.
    public damageType myType;
    //myMesh is the mesh used when the player uses the gun. (NOT IMPLEMENTED YET)
    public GameObject myMesh;
    //myAudio is the audio that plays when the gun is fired
    public AudioSource myAudio;


    //Determine reloading values
    //timetoReloadSingleShot is the amount of time used to reload a single bullet into the chamber.  This is the time between bullets being loaded.
    public float timeToReloadSingleShot;
    //maxAmmo is the max amount of ammo in the gun
    public int maxAmmo;
    //TimeBetween shots is fire rate of gun
    public float timeBetweenShots;

    public bool isDualWeapon;

    //Firing Variables for different guns
    //isSingleShot is if the gun is a dualRevolver or rifle.
    public bool isSingleShot = true;
    //spread is only for shotguns
    public bool isSpread = false;

   
    //This spread determines how wide apart the shots are
    [Range(0.0f,10.0f)]
    public float spread;
    [Range(0, 10)]
    public int shotsInSpread;

    
}
