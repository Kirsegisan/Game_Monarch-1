using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothUpDownMovement : MonoBehaviour
{
    [SerializeField] private float movementRange = 1.0f;
    [SerializeField] private float movementSpeed = 1.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * movementSpeed) * movementRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
