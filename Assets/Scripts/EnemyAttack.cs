using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed = 1f;
    [SerializeField] private float _timeBetweenAttacks = 2f;
    [SerializeField] private float _repelRadius = 5f; // Радиус отталкивания
    [SerializeField] private float _repelForce = 10f; // Сила отталкивания
    [SerializeField] private bool _ranged = true;
    [SerializeField] private bool _forceAttack = true;

    private bool _alreadyAttacked;

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

    private void RepelPlayers()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _repelRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Vector3 repelDirection = (col.transform.position - transform.position);
                float distance = Vector3.Distance(col.transform.position, transform.position);
                float repelStrength = _repelForce / (distance * distance);
                Rigidbody rb = col.GetComponentInParent<Rigidbody>();
                Debug.Log($"{rb != null}; direction: {repelDirection}; strength: {repelStrength}");
                rb.AddForce(repelDirection * repelStrength, ForceMode.Impulse);
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
