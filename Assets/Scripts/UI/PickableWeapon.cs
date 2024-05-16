using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Interactive
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Transform _parentObject;
    [SerializeField] private Transform _referenceWeapon;
    [SerializeField] private WeaponSwitch weaponSwitch;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private WeaponGiver giver;

    public override void Interact()
    {
        base.Interact();
        giver.SpawnWeapon(_objectToSpawn, _parentObject, _referenceWeapon, weaponSwitch, playerData);
        Destroy(gameObject);
    }


}

