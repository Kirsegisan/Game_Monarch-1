using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public bool canShoot = true;
    public bool infinitAmmo = true;
    public float fireRate = 1f;
    public float recoilSpeed = 2f;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    //Реализация стрельбы очередями
    public float countBulletInBurst = 5;
    public float countBulletInBurstNow;
    public float timeBetweenShoot = 0.1f;
    public float angleTimeBetweenShoot = 0;
    public float timeBetweenBurst = 1;
    public float angleTimeBetweenBurst = 0;
    public float hitCoefficient = 6;//коэффициент в формуле расчета разброса (чем больше, тем точнее выстрел)
    public int fireMode = 0;

    public bool isShooting;

    private void Start()
    {
        isShooting = false;
        countBulletInBurstNow = countBulletInBurst;
         
    }


    private void Update()
    {
        angleTimeBetweenShoot += Time.deltaTime;
        angleTimeBetweenBurst += Time.deltaTime;
        
        if (angleTimeBetweenBurst >= timeBetweenBurst && angleTimeBetweenShoot >= timeBetweenShoot)
        {
            canShoot = true;
            if(countBulletInBurstNow == 0)
            { countBulletInBurstNow = countBulletInBurst; }
            
        }

        if (!infinitAmmo)
        {
            if (GlobalVariables.cBulletsAmount < 1)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }
        }

        if (isShooting)
        {
                Shoot();
        }
        
    }


    public void StartShooting()
    {
        if (!isShooting & canShoot)
        {
            isShooting = true;
            //StartCoroutine(ShootCoroutine());
        }
    }

    public void StopShooting()
    {
        if (isShooting)
        {
            countBulletInBurstNow = 0;
            angleTimeBetweenBurst = 0;
            canShoot = false;

            isShooting = false;
            //StopCoroutine(ShootCoroutine());
        }
    }

    /*private IEnumerator ShootCoroutine()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(1f / fireRate);
        }
    }*/

    private void Shoot()
    {
        if (canShoot)
        {
            //Обнуление счетчиков
            canShoot = false;
            countBulletInBurstNow -= 1;
            if (!infinitAmmo)
            {
                GlobalVariables.cBulletsAmount -= 1;
            }
            angleTimeBetweenShoot = 0;

            if(countBulletInBurstNow <= 0) { angleTimeBetweenBurst = 0;}
            

            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            Vector3 lookDirection = Camera.main.transform.forward;
            //Пользуясь особенностью задавания направления через вектор, задается небольшой случайный прирост
            //Так же зависящий от количества сделанных выстрелов
            lookDirection.z += Convert.ToSingle(UnityEngine.Random.Range(-1, 1) / (hitCoefficient + countBulletInBurstNow) * Math.Sin(Math.PI / 2 * lookDirection.z));
            lookDirection.y += Convert.ToSingle(UnityEngine.Random.Range(-1, 1) / (hitCoefficient + countBulletInBurstNow) * Math.Sin(Math.PI / 2 * lookDirection.y));
            lookDirection.x += Convert.ToSingle(UnityEngine.Random.Range(-1, 1) / (hitCoefficient + countBulletInBurstNow) * Math.Sin(Math.PI / 2 * lookDirection.x));
            lookDirection.Normalize();
            bullet.transform.forward = lookDirection;
            
            //recoil.ApplyRecoil();
        }
    }
}

