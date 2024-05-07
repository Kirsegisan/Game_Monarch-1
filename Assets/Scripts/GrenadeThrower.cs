using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public float throwForce = 20f;
    public int maxBounces = 3;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = throwPoint.forward * throwForce;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on grenade prefab!");
        }
    }
}
