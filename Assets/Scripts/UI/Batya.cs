using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batya : Interactive
{
    // Батя
    [SerializeField] private PlayerData _playerData;
    public override void Interact()
    {
        base.Interact();
        _playerData.health = _playerData.maxHealth;
        _playerData.ammo += 100;
    }

}
