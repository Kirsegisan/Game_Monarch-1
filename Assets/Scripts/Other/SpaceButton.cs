using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceButton : Interactive
{
    private bool _interactable = true;
    private Vector3 _originalPosition;
    private Vector3 _pressedPosition;

    [SerializeField] private float _pressDepth = 0.1f;
    [SerializeField] private float _cooldownDuration = 2f;
    [SerializeField] private SpaceButtonHandler _handler;

    public bool pushed;

    private float _lastInteractionTime;

    private void Start()
    {
        _originalPosition = transform.position;
        _pressedPosition = _originalPosition - transform.up * _pressDepth;
    }

    public override void Interact()
    {
        if (_interactable && Time.time - _lastInteractionTime >= _cooldownDuration)
        {
            _lastInteractionTime = Time.time;
            PressButton();
        }
    }

    private void PressButton()
    {
        _interactable = false;
        _handler.ButtonPressed();
        StartCoroutine(PressAnimation());
    }

    private IEnumerator PressAnimation()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.1f)
        {
            transform.position = Vector3.Lerp(_originalPosition, _pressedPosition, elapsedTime / 0.1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        elapsedTime = 0f;
        while (elapsedTime < 0.1f)
        {
            transform.position = Vector3.Lerp(_pressedPosition, _originalPosition, elapsedTime / 0.1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _interactable = true;
        pushed = false;
    }

    public bool IsPushed()
    {
        return pushed;
    }
}
