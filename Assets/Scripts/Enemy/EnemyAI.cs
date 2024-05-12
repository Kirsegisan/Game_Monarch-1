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
    [SerializeField] private float attackRange = 2f; // Радиус атаки
    [SerializeField] private float sightRange = 10f; // Радиус обнаружения игрока

    // Параметры для патрулирования
    protected Vector3 walkPoint;
    protected bool walkPointSet;

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
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Проверка на достижение точки патрулирования
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    protected virtual void SearchWalkPoint()
    {
        // Генерация случайной точки патрулирования
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
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
