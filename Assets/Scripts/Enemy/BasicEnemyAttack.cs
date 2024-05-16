using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] protected float _timeBetweenAttacks = 2f;
    protected bool _alreadyAttacked;

    public virtual void Attack(Transform target)
    {
        if (_alreadyAttacked)
            return;

        PerformAttack(target);

        _alreadyAttacked = true;
        Invoke(nameof(ResetAttack), _timeBetweenAttacks);
    }

    protected virtual void PerformAttack(Transform target)
    {
        // Реализация атаки в подклассах
    }

    protected void ResetAttack()
    {
        _alreadyAttacked = false;
    }
}

