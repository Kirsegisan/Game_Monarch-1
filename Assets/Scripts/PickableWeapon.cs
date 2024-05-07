using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Pickable
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform parentObject;
    [SerializeField] private Transform referenceWeapon;

    public override void Interact()
    {
        base.Interact();

        if (objectToSpawn != null && parentObject != null && referenceWeapon != null)
        {
            GameObject newObject = Instantiate(objectToSpawn, parentObject);
            // Устанавливаем позицию, поворот и размер нового объекта такими же, как у начальной пушки
            newObject.transform.localPosition = referenceWeapon.localPosition;
            newObject.transform.localRotation = referenceWeapon.localRotation;
            newObject.transform.localScale = referenceWeapon.localScale;

            // Отключаем все дочерние объекты, кроме только что созданного
            foreach (Transform child in parentObject)
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

