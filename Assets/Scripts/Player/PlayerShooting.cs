using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Shooter shooter;
    public bool isAutoFiring = false;
    private float fireRate;
    private float nextFireTime = 0f;

    [Header("Healing Settings")]
    [SerializeField] private float healingCooldown = 5f; // Кулдаун хилки в секундах
    private bool canHealing = true;
    private float angleHealingCooldown = 0;
    [SerializeField] private float heal = 10f;

    [Header("Animation and Sound")]
    private Animator handAnimator;
    private AudioSource shootSound;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        playerData.health = playerData.maxHealth;
        handAnimator = GetComponent<Animator>();
        shootSound = GetComponent<AudioSource>();
        fireRate = shooter.fireRate;
       
    }

    public void HealingAndDamage(float damage, float healAmount)
    {
        playerData.health -= damage - healAmount;
    }

    void Update()
    {
        fireRate = shooter.fireRate;

        if (!canHealing)
        {
            angleHealingCooldown -= Time.deltaTime;
            HealingAndDamage(0, heal * angleHealingCooldown * angleHealingCooldown);
            if (angleHealingCooldown <= 0) { canHealing = true; }
        }

        if (playerData.health <= 0)
        {
            //gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H) && canHealing)
        {
            handAnimator.SetBool("Healing", true);
            canHealing = false;
            angleHealingCooldown = healingCooldown;
        }

        if (isAutoFiring && Time.time >= nextFireTime && Input.GetButton("Fire1"))
        {
            shooter.Fire();
            if (shootSound != null)
                shootSound.Play();
            handAnimator.SetBool("shoot", true);
            nextFireTime = Time.time + 1f / fireRate;
        }
        else if (Input.GetButtonDown("Fire1") && !isAutoFiring)
        {
            shooter.Fire();
            if (shootSound != null)
                shootSound.Play();
            handAnimator.SetBool("shoot", true);
        }
    }
}
