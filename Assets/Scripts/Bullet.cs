﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float destructionDelay = 5f; // Задержка перед уничтожением пули

    private void Start()
    {
        Destroy(gameObject, destructionDelay); // Уничтожение пули через указанное время
    }

    public float getDamage() { return damage; }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
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
            if (parent.CompareTag("Player"))
            {

                other.GetComponent<PlayerController>().HealingAndDamage(damage, 0);

                if (playerData.health <= 0)
                {
                    // В душе не ебу зачем тут это
                }

            }
            Destroy(gameObject); // Уничтожение пули при столкновении
        }
    }
}
