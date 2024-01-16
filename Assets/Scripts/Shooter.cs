using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float fireRate = 1f;
    public float recoilSpeed = 2f;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public Recoil recoil;

    private bool isShooting;

    private void Start()
    {
        isShooting = false;
    }

    public void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootCoroutine());
        }
    }

    public void StopShooting()
    {
        if (isShooting)
        {
            isShooting = false;
            StopCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Vector3 lookDirection = Camera.main.transform.forward;
        bullet.transform.forward = lookDirection;
        recoil.ApplyRecoil();
    }
}
