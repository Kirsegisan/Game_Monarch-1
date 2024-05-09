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
    private bool canHealing = true;
    [SerializeField] private float healingCooldown; // Кулдаун хилки в секундах
    private float angleHealingCooldown = 0;
    public bool isAutoFiring = false;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    private Animator handAnimator;
    private AudioSource ShootSound;
    [SerializeField] private float heal;

    [SerializeField] PlayerData playerData;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerData.health = playerData.maxHealth;


        handAnimator = GetComponent<Animator>();
        ShootSound = GetComponent<AudioSource>();
    }

    public void HealingAndDamage(float damage, float heal)
    {
        playerData.health -= damage - heal;
    }


    void Update()
    {
        // счетчики кулдауна
        if (!canHealing) { angleHealingCooldown -= Time.deltaTime;
            HealingAndDamage(0, heal * angleHealingCooldown * angleHealingCooldown);
            if (angleHealingCooldown <= 0) { canHealing = true; } }



        // ну и все остальное

        if (playerData.health <= 0)
        {
            gameObject.SetActive(false);
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
        if (z != 0 || x != 0) { handAnimator.SetBool("isRunning", true); }
        else { handAnimator.SetBool("isRunning", false); }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isAutoFiring = !isAutoFiring;
        }


        if (Input.GetKeyDown(KeyCode.H) && canHealing)
        {
            handAnimator.SetBool("Healing", true);
            canHealing = false; angleHealingCooldown = healingCooldown;
        }


        if (Input.GetKeyDown(KeyCode.F) && Input.GetKeyDown(KeyCode.U)&&
            Input.GetKeyDown(KeyCode.C) && Input.GetKeyDown(KeyCode.K))
        {
            HealingAndDamage(playerData.health - 1, 0);
        }

        if (isAutoFiring && Time.time >= nextFireTime && Input.GetButton("Fire1"))
        {
            shooter.Fire();
            ShootSound.Play();
            handAnimator.SetBool("shoot", true);
            nextFireTime = Time.time + 1f / fireRate;
        }
        else if (Input.GetButtonDown("Fire1") && !isAutoFiring)
        {
            shooter.Fire();
            handAnimator.SetBool("shoot", true);
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
