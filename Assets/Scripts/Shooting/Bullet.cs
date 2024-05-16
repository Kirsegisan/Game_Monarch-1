using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float destructionDelay = 2f; // Задержка перед уничтожением пули
    public string creator = "God";
    public LayerMask creatorLayer;

    private void Start()
    {
        Destroy(gameObject, destructionDelay); // Уничтожение пули через указанное время
    }

    public float GetDamage() { return damage; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On bullet trigger: " + other.gameObject.name);
        Transform parent = other.transform.parent;
        if (parent)
        {
            if (parent.CompareTag("Enemy"))
            {
                ObjectData objectData = parent.GetComponent<ObjectData>();
                if (objectData)
                {
                    objectData.currentHealth -= damage;

                    if (objectData.currentHealth <= 0)
                    {
                        // Для разных типов пуль можно сделать разные добивания
                    }
                }
            }
            else if (parent.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().HealingAndDamage(damage, 0);
            }


            Debug.Log("Bullet destroyed: " + other.gameObject.name);
            Destroy(gameObject);

        }
    }
}
