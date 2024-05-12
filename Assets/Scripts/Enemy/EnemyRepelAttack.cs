using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyRepelAttack : BasicEnemyAttack
{
    [SerializeField] protected float _repelRadius = 5f; // Радиус отталкивания
    [SerializeField] protected float _repelForce = 10f; // Сила отталкивания
    [SerializeField] protected float _stanTime = 1.0f; // Время оглушения при отталкивании

    private PlayerMovement _movement;

    protected override void PerformAttack(Transform target)
    {
        RepelPlayers();
    }

    protected virtual void RepelPlayers()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _repelRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                _movement = col.gameObject.GetComponentInParent<PlayerMovement>();
                _movement.SetInflunce(true);
                StartCoroutine(ResetIsInfluenced());
                Vector3 repelDirection = (col.transform.position - transform.position);
                float distance = Vector3.Distance(col.transform.position, transform.position);
                if (distance > 0)
                {
                    float repelStrength = _repelForce / distance;
                    Rigidbody rb = col.GetComponentInParent<Rigidbody>();
                    rb.velocity = repelDirection.normalized * repelStrength;
                }
            }
        }
    }

    private IEnumerator ResetIsInfluenced()
    {
        yield return new WaitForSeconds(_stanTime);
        if (_movement != null)
            _movement.SetInflunce(false);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _repelRadius);
        }
    }
}
