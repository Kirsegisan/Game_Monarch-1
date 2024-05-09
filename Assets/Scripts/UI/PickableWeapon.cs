using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Interactive
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Transform _parentObject;
    [SerializeField] private Transform _referenceWeapon;

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
            foreach (Transform child in _parentObject)
            {
                if (child.gameObject != newObject)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        Destroy(gameObject);
    }
}

