using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    [Header("Camera")]
    [SerializeField] private Camera[] _playerCameras;
    [SerializeField] private float _lookSensitivity = 2f;
    [SerializeField] private float _maxLookX = 45f;
    [SerializeField] private float _minLookX = -45f;

    public bool isInfluenced = false;

    private Vector3 _velocity;
    private bool _isGrounded;

    private Animator _handAnimator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();
        RotateCharacter();
        MovePlayer();
        HandleJump();
        RotateCamera();
        HandleInteractions();
    }

    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void MovePlayer()
    {
        if (!isInfluenced && _isGrounded)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            _rb.velocity = move * _speed;

            // Animation control
            _handAnimator.SetBool("isRunning", move.magnitude > 0);
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;
        _rb.velocity = new Vector3(_rb.velocity.x, _velocity.y, _rb.velocity.z);
    }

    private void RotateCharacter()
    {
        float rotY = Input.GetAxis("Mouse X") * _lookSensitivity;
        Quaternion deltaRotation = Quaternion.Euler(0, rotY, 0);
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    }

    private void RotateCamera()
    {
        float rotX = -Input.GetAxis("Mouse Y") * _lookSensitivity;
        rotX = Mathf.Clamp(rotX, _minLookX, _maxLookX);

        foreach (Camera cam in _playerCameras)
        {
            cam.transform.localRotation *= Quaternion.Euler(rotX, 0, 0);
        }
    }

    private void HandleInteractions()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Press F
        }
    }

    public void SetInflunce(bool inflated)
    {
        isInfluenced = inflated;
    }
}

