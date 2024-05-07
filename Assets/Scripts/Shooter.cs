using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float spread = 0.1f;

    void Awake()
    {
        GameObject firePointObject = GameObject.FindGameObjectWithTag("FirePoint");
        if (firePointObject != null)
        {
            firePoint = firePointObject.transform;
        }
        else
        {
            Debug.LogError("FirePoint не найден. Добавьте объект FirePoint с тегом 'FirePoint'.");
        }
    }

    public void Fire()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);

        Vector3 bulletStartPosition = firePoint.position;
        Vector3 bulletDirection = firePoint.forward;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            bulletStartPosition = hit.point;
            bulletDirection = (hit.point - firePoint.position).normalized;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Vector3 spreadVector = Random.insideUnitSphere * spread;
        if (rb != null)
        {
            rb.AddForce(bulletDirection * bulletForce + spreadVector, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
        }
    }
}
