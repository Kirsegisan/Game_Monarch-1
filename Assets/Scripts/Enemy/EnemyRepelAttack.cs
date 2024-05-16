using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRepelAttack : BasicEnemyAttack
{
    [SerializeField] protected float _repelRadius = 5f; // Радиус отталкивания
    [SerializeField] protected float _repelForce = 100f; // Сила отталкивания
    [SerializeField] protected float _stanTime = 1.0f; // Время оглушения при отталкивании
    [SerializeField] protected GameObject _repelFieldPrefab; // Префаб шара силового поля
    [SerializeField] protected float _kfSize = 1f;
    [SerializeField] protected float _kfDamage = 1f;
    [SerializeField] protected bool _selfDistruction = false;
    [SerializeField] protected bool _damageEnabled = false;

    private PlayerMovement _movement;
    private GameObject _repelField;

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
                _movement.SetInfluence(true);
                StartCoroutine(ResetIsInfluenced());
                Vector3 repelDirection = (col.transform.position - transform.position);
                float distance = Vector3.Distance(col.transform.position, transform.position);
                if (distance > 0)
                {
                    float repelStrength = _repelForce / distance;
                    if (_damageEnabled)
                    {
                        col.GetComponentInParent<PlayerShooting>().HealingAndDamage(repelStrength * _kfDamage, 0);
                    }
                    Rigidbody rb = col.GetComponentInParent<Rigidbody>();
                    rb.velocity = repelDirection.normalized * repelStrength;
                    CreateRepelField();
                }
            }
        }
    }

    private IEnumerator ResetIsInfluenced()
    {
        yield return new WaitForSeconds(_stanTime);
        if (_movement != null)
            _movement.SetInfluence(false);
    }

    private void CreateRepelField()
    {
        if (_repelFieldPrefab != null)
        {
            _repelField = Instantiate(_repelFieldPrefab, transform.position, Quaternion.identity);
            StartCoroutine(ExpandRepelField());
        }
    }

    private IEnumerator ExpandRepelField()
    {
        float currentRadius = 0f;
        while (currentRadius < _repelRadius)
        {
            currentRadius += Time.deltaTime * (_repelRadius / _stanTime) * _kfSize; // увеличиваем радиус шара со временем
            _repelField.transform.localScale = new Vector3(currentRadius, currentRadius, currentRadius);
            yield return null;
        }

        Destroy(_repelField);
        if (_selfDistruction)
        {
            _movement.SetInfluence(false);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _repelRadius);
        }
    }

    private void OnDestroy()
    {
        Destroy(_repelField);
    }
}
