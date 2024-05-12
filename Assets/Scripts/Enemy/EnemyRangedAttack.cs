using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyRangedAttack : BasicEnemyAttack
{
    //[SerializeField] protected GameObject _projectile; // ������ �������
    //[SerializeField] protected Transform _firePoint; // ����� ������� �������
    //[SerializeField] protected float _projectileSpeed = 1f; // �������� �������
    [SerializeField] protected Shooter _shooter;

    protected override void PerformAttack(Transform target)
    {
        //if (_projectile != null && _firePoint != null)
        //{
        //    Vector3 attackDirection = (target.position - _firePoint.position).normalized;
        //    Rigidbody projectileInstance = Instantiate(_projectile, _firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        //    projectileInstance.velocity = attackDirection * _projectileSpeed;
        //}
        _shooter.Fire();
    }
}
