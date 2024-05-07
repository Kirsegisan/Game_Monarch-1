using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float destructionDelay = 2f;
    public string creator = "God";
    public LayerMask creatorLayer;

    private void Start()
    {
        StartCoroutine(ExplodeCoroutine());
    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(destructionDelay);
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            ObjectData objectData = hit.GetComponent<ObjectData>();
            if (objectData != null)
            {
                objectData.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    public float GetDamage() { return damage; }
}
