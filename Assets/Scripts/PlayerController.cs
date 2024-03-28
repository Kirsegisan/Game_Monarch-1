using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Camera playerCamera;
    public float lookSensitivity = 2f;
    public float maxLookX = 45f;
    public float minLookX = -45f;
    private float rotX;

    public Shooter shooter;
    public bool isAutoFiring = false;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Update()
    {
        // Проверка нахождения на земле
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Управление движением
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Управление прыжком
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Управление стрельбой
        // Переключение режима стрельбы
        if (Input.GetKeyDown(KeyCode.F)) // F - кнопка для переключения
        {
            isAutoFiring = !isAutoFiring;
        }

        // Автоогонь
        if (isAutoFiring && Time.time >= nextFireTime && Input.GetButton("Fire1"))
        {
            shooter.Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
        else if (Input.GetButtonDown("Fire1") && !isAutoFiring)
        {
            shooter.Fire();
        }

        // Управление гравитацией
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Управление камерой
        rotX += -Input.GetAxis("Mouse Y") * lookSensitivity;
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        transform.eulerAngles += Vector3.up * Input.GetAxis("Mouse X") * lookSensitivity;
    }
}
