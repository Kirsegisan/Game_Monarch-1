using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TMP_Text healsText;

    private void Start()
    {
        UpdateHealsDisplay();
    }

    public void UpdateHealsDisplay()
    {
        if (playerData != null && healsText != null)
        {
            healsText.text = $"{playerData.heals} | {playerData.maxHeals}";
        }
    }
}
