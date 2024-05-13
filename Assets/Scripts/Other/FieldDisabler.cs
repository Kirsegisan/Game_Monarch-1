using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDisabler : SpaceButtonHandler
{
    [SerializeField] private GameObject _field;
    private bool _isActive = true;

    public override void ButtonPressed()
    {
        base.ButtonPressed();
        _isActive = !_isActive;
        _field.SetActive(_isActive);
    }
}
