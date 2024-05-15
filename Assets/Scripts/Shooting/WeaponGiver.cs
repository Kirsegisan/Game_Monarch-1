using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGiver : MonoBehaviour
{
    public void SpawnWeapon(GameObject objectToSpawn, Transform parentObject, Transform referenceWeapon, WeaponSwitch weaponSwitch, PlayerData playerData)
    {
        if (objectToSpawn != null && parentObject != null && referenceWeapon != null)
        {
            GameObject newObject = Instantiate(objectToSpawn, parentObject);
            // ������������� �������, ������� � ������ ������ ������� ������ ��, ��� � ��������� �����
            newObject.transform.localPosition = referenceWeapon.localPosition;
            newObject.transform.localRotation = referenceWeapon.localRotation;
            newObject.transform.localScale = referenceWeapon.localScale;

            weaponSwitch.SwitchToNextChild();
            Shooter shooter = newObject.GetComponent<Shooter>();
            if (shooter != null && shooter.playerData == null)
                shooter.playerData = playerData;
        }
    }
}
