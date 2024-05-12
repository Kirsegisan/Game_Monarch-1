using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab; // Префаб снаряда
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float maxDamage = 10f;
    [SerializeField] private float damageFalloff = 0.1f;

    private float rayDistance = 999f;

    private const string firePointTag = "FirePoint";

    private void Start()
    {
        if (firePoint == null)
        {
            FindFirePoint();
        }
    }

    private void FindFirePoint()
    {
        GameObject firePointObject = GameObject.FindGameObjectWithTag(firePointTag);
        if (firePointObject != null)
            firePoint = firePointObject.transform;
        else
            Debug.LogError("FirePoint not found. Add a FirePoint object with tag 'FirePoint'.");
    }

    public void Fire()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned.");
            return;
        }
        if (this.isActiveAndEnabled)
        {
            Ray ray = new Ray(firePoint.position, firePoint.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                //Debug.Log($"{this.name} {hit.transform.name}");
                float travelTime = hit.distance / bulletSpeed;
                ApplyDamageDelayed(hit, travelTime);

                // Создание и запуск движения снаряда
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = firePoint.forward * bulletSpeed;
                    Destroy(bullet, travelTime); // Уничтожение снаряда после указанного времени
                }
            }
        }
    }

    private void ApplyDamageDelayed(RaycastHit hit, float travelTime)
    {
        StartCoroutine(DelayedDamageCoroutine(hit, travelTime));
    }

    private IEnumerator DelayedDamageCoroutine(RaycastHit hit, float travelTime)
    {
        yield return new WaitForSeconds(travelTime);
        ApplyDamage(hit);
    }

    private void ApplyDamage(RaycastHit hit)
    {
        float damage = CalculateDamage(hit.distance);
        if (hit.transform != null)
        {
            if (hit.transform.CompareTag("Enemy") && gameObject.tag != "Enemy")
            {
                ObjectData objectData = hit.transform.GetComponent<ObjectData>();
                if (objectData)
                {
                    Debug.Log(damage);
                    objectData.TakeDamage(damage);
                }
            }
            else if (hit.transform.CompareTag("Player") && gameObject.tag != "Player")
            {
                hit.transform.GetComponent<PlayerShooting>()?.HealingAndDamage(damage, 0);
            }
        }

    }

    private float CalculateDamage(float distance)
    {
        float falloffMultiplier = Mathf.Clamp01(maxDistance / distance);
        float actualDamage = maxDamage * falloffMultiplier * (1 / damageFalloff);
        return actualDamage;
    }
}
