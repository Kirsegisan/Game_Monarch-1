using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TMP_Text ammoText;

    private void Start()
    {
        UpdateAmmoDisplay();
    }

    public void UpdateAmmoDisplay()
    {
        if (playerData != null && ammoText != null)
        {
            ammoText.text = playerData.ammo.ToString();
        }
    }
}


