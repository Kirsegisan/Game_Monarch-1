using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyAI : EnemyAI
{
    [SerializeField] private SpiderAnimation spiderAnimation;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void ChasePlayer()
    {
        base.ChasePlayer();
        spiderAnimation.Run();
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        spiderAnimation.Shoot();
    }

    protected override void Patrolling()
    {
        base.Patrolling();
        spiderAnimation.Run();
    }
}

