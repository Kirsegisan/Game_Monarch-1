using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 1f;
    [SerializeField] private float _timeBetweenAttacks = 2f;
    [SerializeField] private float _repelRadius = 5f; // Радиус отталкивания
    [SerializeField] private float _repelForce = 10f; // Сила отталкивания
    [SerializeField] private float _stanTime = 1.0f;
    [SerializeField] private bool _ranged = true;
    [SerializeField] private bool _forceAttack = true;

    private bool _alreadyAttacked;

    private PlayerMovement _movement;

    private void Start()
    {
        if (_firePoint == null && _ranged)
        {
            Debug.LogError("Fire point is not assigned in EnemyAttack script!", this);
            enabled = false;
        }
    }

    public void Attack(Transform target)
    {
        if (_alreadyAttacked)
            return;

        if (_forceAttack)
            RepelPlayers();

        if (_projectile != null && _ranged)
            LaunchProjectile(target);

        _alreadyAttacked = true;
        Invoke(nameof(ResetAttack), _timeBetweenAttacks);
    }

    private IEnumerator ResetIsInfluenced()
    {
        yield return new WaitForSeconds(_stanTime);
        if (_movement != null)
            _movement.SetInflunce(false);
    }

    private void RepelPlayers()
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

    private void LaunchProjectile(Transform target)
    {
        Vector3 attackDirection = (target.position - _firePoint.position).normalized;
        Rigidbody projectileInstance = Instantiate(_projectile, _firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectileInstance.velocity = attackDirection * _projectileSpeed;
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _repelRadius);
        }
    }
}
