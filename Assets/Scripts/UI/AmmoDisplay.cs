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
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        if (playerData != null && ammoText != null)
        {
            ammoText.text = playerData.ammo.ToString();
        }
    }
}


