using UnityEngine;
using System;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;
    public float chaseRange = 10.0f;
    public float attackRange = 2.0f;
    public float attackDamage = 2.0f;
    public float attackCooldown = 1.0f;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool isDead = false;
    private float nextAttackTime = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
 
        float distance = Vector3.Distance(transform.position, player.position);


        if (distance <= attackRange)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }


        if (distance <= chaseRange)
        {

            isWalking = true;
            navMeshAgent.SetDestination(player.position);
        }
        else
        {

            isWalking = false;
            navMeshAgent.SetDestination(transform.position);
        }


        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsAttacking", isAttacking);
        animator.SetBool("IsDead", isDead);


    }

}
