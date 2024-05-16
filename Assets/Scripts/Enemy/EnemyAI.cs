using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent; // Ссылка на NavMeshAgent врага
    [SerializeField] private Transform player; // Ссылка на игрока
    [SerializeField] private LayerMask whatIsGround; // Слой для определения земли
    [SerializeField] private LayerMask whatIsPlayer; // Слой для определения игрока

    [Header("Patrol Parameters")]
    [SerializeField] private float walkPointRange = 10f; // Радиус, в котором генерируются точки патрулирования
    [SerializeField] private float rotationSpeed = 1f;

    [Header("Combat Parameters")]
    [SerializeField] private float attackRange = 2f; // Радиус в которой враг остановится и начнет атаковать
    [SerializeField] private float sightRange = 10f; // Радиус обнаружения игрока

    // Ссылка на скрипт атаки
    protected BasicEnemyAttack enemyAttack;

    protected virtual void Awake()
    {
        // Находим игрока и получаем ссылки на компоненты
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject fObj in players)
        {
            PlayerMovement playerMovement = fObj.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                player = fObj.transform;
            }
        }
        agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<BasicEnemyAttack>();
    }

    protected virtual void Update()
    {
        // Проверка на наличие игрока в области видимости и радиусе атаки
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    protected virtual void Patrolling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SearchRandomPatrolPoint();
        }
    }

    protected virtual void SearchRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkPointRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkPointRange, 1);
        Vector3 finalPosition = hit.position;

        agent.SetDestination(finalPosition);
    }

    protected virtual void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    protected virtual void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (enemyAttack != null && player != null)
            enemyAttack.Attack(player);
    }
}
