using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int maxHeals = 2;
    public float maxHealth = 100;
    private float _health = 100;
    private int _heals = 2;
    public int ammo = 9999;

    public float health
    {
        get { return _health; }
        set { _health = Mathf.Clamp(value, 0, maxHealth); }
    }

    public int heals
    {
        get { return _heals; }
        set { _heals = Mathf.Clamp(value, 0, maxHeals); }
    }

    void OnValidate()
    {
        health = _health;
        heals = _heals;
    }
}
