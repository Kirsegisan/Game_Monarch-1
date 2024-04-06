using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; // Ссылка на объект слайдера
    [SerializeField] public float maxHealth; // Максимальное значение здоровья
    [SerializeField] private float currentHealth; // Текущее значение здоровья
    [SerializeField] private ObjectData objectData;
    [SerializeField] private PlayerData playerData;

    void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (objectData)
        {
            currentHealth = objectData.currentHealth;
            maxHealth = objectData.maxHealth;
        }
        else if (playerData)
        {
            currentHealth = playerData.health;
            maxHealth = playerData.maxHealth;
        }
        healthSlider.value = currentHealth / maxHealth;
    }
}

