using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingStation : Interactive
{
    [Header("Restoration parameters")]
    [SerializeField] private float healthRestorationPercent = 0.7f;
    [SerializeField] private int ammoRestorationAmount = 100;
    [SerializeField] public int restorationsAmount = 2;

    [Header("Technical")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AmmoDisplay ammoDisplay;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private HealsDisplay healsDisplay;

    public override void Interact()
    {
        base.Interact();
        if (restorationsAmount > 0)
        {
            playerData.health += playerData.maxHealth * healthRestorationPercent;
            playerData.ammo += ammoRestorationAmount;
            playerData.heals = playerData.maxHeals;
            healsDisplay.UpdateHealsDisplay();
            ammoDisplay.UpdateAmmoDisplay();
            healthBar.UpdateHealthBar();
            restorationsAmount -= 1;
        }

        if (restorationsAmount == 0)
        {
            objectName = "Ресурс исчерпан";
            interactionButton = "";
        }
    }
}
