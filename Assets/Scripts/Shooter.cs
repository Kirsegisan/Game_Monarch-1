using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float spread = 0.1f;

    public void Fire()
    {
        // Ñîçäàíèå ïóëè
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Äîáàâëåíèå ðàçáðîñà
        Vector3 spreadVector = new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            Random.Range(-spread, spread)
        );

        if (rb != null)
        {
            // Ïðèìåíåíèå ñèëû
            rb.AddForce(firePoint.forward * bulletForce + spreadVector, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
        }
    }
}
