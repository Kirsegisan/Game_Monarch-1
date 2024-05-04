using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Ïàðàìåòðû ïàòðóëèðîâàíèÿ
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Ïàðàìåòðû àòàêè
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    [SerializeField] private float projectileSpeed = 1f;

    // Ñîñòîÿíèÿ
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [SerializeField] private Transform firePoint;
    [SerializeField] private SphereCollider attackCollider;

    [SerializeField] private float stepToAttack;
    private float minToAttack = 0.1f;
    private bool isAttack = false;
    [SerializeField] private float attackDastans;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Ïðîâåðêà íà âèäèìîñòü è äèñòàíöèþ àòàêè
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();


        //счетчик атаки
        if (isAttack)
        {
            attackCollider.radius += stepToAttack * Time.deltaTime;
            if (attackCollider.radius > attackDastans) { isAttack = false; attackCollider.radius = minToAttack; }
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Äîñòèæåíèå òî÷êè
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Ãåíåðàöèÿ ñëó÷àéíîé òî÷êè äëÿ ïàòðóëèðîâàíèÿ
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            isAttack = true;
           /* Vector3 attackDirection = (player.position - firePoint.position).normalized;

            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = attackDirection * projectileSpeed;*/
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

}
