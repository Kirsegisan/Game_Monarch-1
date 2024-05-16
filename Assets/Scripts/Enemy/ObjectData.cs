using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [SerializeField] public int bullets = 100;
    [SerializeField] public bool infiniteAmmo = true;
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private HealthBar healthBar;

    protected virtual void Update()
    {
        if ((currentHealth <= 0 || maxHealth <= 0))
        {
            Die();
        }
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar();
        }
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
