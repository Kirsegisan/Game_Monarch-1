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

    public Camera[] playerCameras; // Массив камер
    public float lookSensitivity = 2f;
    public float maxLookX = 45f;
    public float minLookX = -45f;
    private float rotX;

    public Shooter shooter;
    public bool isAutoFiring = false;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    [SerializeField] PlayerData playerData;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerData.health = playerData.maxHealth;
    }

    void Update()
    {
        if (playerData.health <= 0)
        {
            //Смерть
        }

        // Ïðîâåðêà íàõîæäåíèÿ íà çåìëå
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Óïðàâëåíèå äâèæåíèåì
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Óïðàâëåíèå ïðûæêîì
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Óïðàâëåíèå ñòðåëüáîé
        // Ïåðåêëþ÷åíèå ðåæèìà ñòðåëüáû
        if (Input.GetKeyDown(KeyCode.F)) // F - êíîïêà äëÿ ïåðåêëþ÷åíèÿ
        {
            isAutoFiring = !isAutoFiring;
        }

        // Àâòîîãîíü
        if (isAutoFiring && Time.time >= nextFireTime && Input.GetButton("Fire1"))
        {
            shooter.Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
        else if (Input.GetButtonDown("Fire1") && !isAutoFiring)
        {
            shooter.Fire();
        }

        // Óïðàâëåíèå ãðàâèòàöèåé
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Óïðàâëåíèå êàìåðîé для каждой камеры в массиве
        foreach (Camera cam in playerCameras)
        {
            rotX += -Input.GetAxis("Mouse Y") * lookSensitivity;
            rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
            cam.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
            transform.eulerAngles += Vector3.up * Input.GetAxis("Mouse X") * lookSensitivity;
        }
    }
}
