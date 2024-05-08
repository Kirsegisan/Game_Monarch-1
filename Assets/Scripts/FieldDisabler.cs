using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDisabler : MonoBehaviour
{
    [SerializeField] private CustomButton _activateButton;
    [SerializeField] private GameObject _field;
    private bool _isActive = true;

    void Update()
    {
        //Debug.Log("Button: " + _activateButton.IsPushed());
        if (_activateButton.IsPushed())
        {
            _isActive = !_isActive;
        }
        _field.SetActive(_isActive);
    }
}
