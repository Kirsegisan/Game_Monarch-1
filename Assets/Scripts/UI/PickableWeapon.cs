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

    private int index = 0;

    public override void Interact()
    {
        base.Interact();

        if (_objectToSpawn != null && _parentObject != null && _referenceWeapon != null)
        {
            GameObject newObject = Instantiate(_objectToSpawn, _parentObject);
            // Устанавливаем позицию, поворот и размер нового объекта такими же, как у начальной пушки
            newObject.transform.localPosition = _referenceWeapon.localPosition;
            newObject.transform.localRotation = _referenceWeapon.localRotation;
            newObject.transform.localScale = _referenceWeapon.localScale;

            // Отключаем все дочерние объекты, кроме только что созданного
            weaponSwitch.SwitchToNextChild();
            newObject.GetComponent<Shooter>().playerData = playerData;
        }
        Destroy(gameObject);
    }
}

