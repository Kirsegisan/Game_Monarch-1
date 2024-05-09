using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public int bullets = 100;
    public bool infiniteAmmo = false;
    public float currentHealth = 100;
    public float maxHealth = 100;
    public bool isDead = false;
    public bool isPlayer;

    private void Update()
    {
        if ((currentHealth <= 0 || maxHealth <= 0) && !isPlayer)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log("damage: " + damage + "hp: " + currentHealth);
    }
}
