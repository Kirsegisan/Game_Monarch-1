using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpecialConditions : MonoBehaviour
{
    [Header("Ktulhu")]
    [SerializeField] private GameObject ktulhuField;
    [SerializeField] private GameObject[] fields;

    [Header("Player")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private WeaponGiver giver;

    [Header("WeaponGiver")]
    [SerializeField] private Transform parentObject;
    [SerializeField] private Transform referenceWeapon;
    [SerializeField] private WeaponSwitch weaponSwitch;

    public void Ktulhu()
    {
        foreach (GameObject field in fields)
        {
            Instantiate(ktulhuField, field.transform.position, field.transform.rotation, field.transform.parent);
            Destroy(field);
        }
    }

    public void GiveWeapon(GameObject weapon)
    {
        giver.SpawnWeapon(weapon, parentObject, referenceWeapon, weaponSwitch, playerData);
    }

    public void GiveAmmo(int ammo)
    {
        playerData.ammo += ammo;
    }

    public void GiveHealth(float health)
    {
        playerData.health += health;
    }
    public void DeleteAfterUse(GameObject thing)
    {
        Destroy(thing);
    }
}
