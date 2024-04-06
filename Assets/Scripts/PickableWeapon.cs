using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Pickable
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform parentObject;

    public override void Interact()
    {
        base.Interact();

        if (objectToSpawn != null && parentObject != null)
        {
            GameObject newObject = Instantiate(objectToSpawn, parentObject);
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

