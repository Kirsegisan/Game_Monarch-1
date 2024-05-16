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
    [SerializeField] private int startAmmo = 100;

    [Header("Healing Settings")]
    [SerializeField] private float healingCooldown = 5f;
    [SerializeField] private float healingTime = 5f;
    private bool canHealing = true;
    private float angleHealingCooldown = 0;
    [SerializeField] private float heal = 10f;

    [Header("Animation and Sound")]
    private Animator handAnimator;
    private AudioSource shootSound;
    [SerializeField] private VoiceAssistant voiceAssistant;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AmmoDisplay ammoDisplay;

    private int activeHealCount = 0;

    private void Start()
    {
        playerData.health = playerData.maxHealth;
        healthBar.UpdateHealthBar();
        playerData.ammo = startAmmo;
        handAnimator = GetComponent<Animator>();
        shootSound = GetComponent<AudioSource>();
        fireRate = shooter.fireRate;
    }

    public void HealingAndDamage(float damage, float healAmount)
    {
        playerData.health -= damage - healAmount;
        voiceAssistant.LowHealth(0.1f);
        healthBar.UpdateHealthBar();
    }

    void Update()
    {
        fireRate = shooter.fireRate;

        if (!canHealing)
        {
            angleHealingCooldown -= Time.deltaTime;
            Debug.Log(angleHealingCooldown);
            if (angleHealingCooldown <= 0)
            {
                canHealing = true;
            }
        }

        if (playerData.health <= 0)
        {
            //gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H) && canHealing && activeHealCount <= 0)
        {
            StartCoroutine(HealOverTime());
        }
        else if (Input.GetKeyDown(KeyCode.H) && canHealing && activeHealCount > 0)
        {
            //StopCoroutine(HealOverTime());
            StartCoroutine(HealOverTime());
        }

        if (isAutoFiring && Time.time >= nextFireTime && Input.GetButton("Fire1"))
        {
            voiceAssistant.LowAmmo(0);
            ammoDisplay.UpdateAmmoText();
            shooter.Fire();
            if (shootSound != null && playerData.ammo - shooter.ammoUse >= 0)
            {
                shootSound.Play();
                handAnimator.SetBool("shoot", true);
            }
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

    IEnumerator HealOverTime(float remainingTime = 0f)
    {
        activeHealCount++;
        canHealing = false;
        handAnimator.SetBool("Healing", true);
        angleHealingCooldown = remainingTime > 0 ? remainingTime : healingCooldown;

        float totalTime = 0f;

        while (totalTime < healingTime)
        {
            float healAmount = heal * Time.deltaTime / healingTime;
            playerData.health = Mathf.Clamp(playerData.health + healAmount, 0, playerData.maxHealth);
            healthBar.UpdateHealthBar();

            totalTime += Time.deltaTime;
            yield return null;
        }

        handAnimator.SetBool("Healing", false);
        activeHealCount--;
    }
}
