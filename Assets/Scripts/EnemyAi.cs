using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float patrolRadius = 10f;
    public float chaseRadius = 20f;
    public float shootingDistance = 15f;
    public float patrolSpeed = 3.0f;
    public float chaseSpeed = 5.0f;
    public float rotationSpeed = 3.0f;
    public float shootCooldown = 2.0f;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Shooter shooter;
    private float timeSinceLastShot;

    private Vector3 patrolPosition;

    void Start()
    {
        patrolPosition = GetRandomPatrolPosition();
        timeSinceLastShot = shootCooldown;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRadius)
        {
            ChasePlayer();
            if (distanceToPlayer < shootingDistance)
            {
                ShootAtPlayer();
            }
        }
        else if (distanceToPlayer > patrolRadius)
        {
            ReturnToPatrol();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        navMeshAgent.speed = patrolSpeed;

        if (Vector3.Distance(transform.position, patrolPosition) < 1.0f)
        {
            patrolPosition = GetRandomPatrolPosition();
        }

        navMeshAgent.SetDestination(patrolPosition);
    }

    void ReturnToPatrol()
    {
        shooter.StopShooting();
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.SetDestination(patrolPosition);
    }

    void ChasePlayer()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);
    }

    void ShootAtPlayer()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shootCooldown)
        {
            shooter.StartShooting();
            timeSinceLastShot = 0f;
        }
    }

    Vector3 GetRandomPatrolPosition()
    {

        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
        return hit.position;
    }
}
