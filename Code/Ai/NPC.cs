using UnityEngine;
using System;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform player;
    public float talkRange = 2.0f;
    public float walkSpeed = 2.0f;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isTalking = false;
    private bool isWalking = false;
    private Vector3 targetDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ?????????????????????? NPC ??????????
        float distance = Vector3.Distance(transform.position, player.position);

        // ???????????????????????????????????????????
        if (distance <= talkRange)
        {
            isTalking = true;
        }
        else
        {
            isTalking = false;
        }

        // ??????? Trigger "IsTalking" ?? Animator
        animator.SetBool("IsTalking", isTalking);

        // ?????????? NPC ??????????????????????
        if (isTalking)
        {
            // ???? NPC 
            navMeshAgent.SetDestination(transform.position);
            // ?????????
            GetComponent<AudioSource>().Play();
        }
        else
        {
            // ?????????? NPC ????????????????????
            if (isWalking)
            {
                // ??? NPC ????
                navMeshAgent.SetDestination(player.position);
                // ????????
                targetDirection = (player.position - transform.position).normalized;
                // ????????????????????
                transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            }
        }

        // ??????? Trigger "IsWalking" ?? Animator
        animator.SetBool("IsWalking", isWalking);

        // ??????????????? Sprite 
        UpdateSpriteDirection();
    }

    public void StartWalking()
    {
        isWalking = true;
    }

    public void StopWalking()
    {
        isWalking = false;
    }

    private void UpdateSpriteDirection()
    {
        // ?????????
        float xDir = targetDirection.x;
        float yDir = targetDirection.y;

        // ??????? Trigger "Direction" ?? Animator
        if (Mathf.Abs(xDir) > Mathf.Abs(yDir))
        {
            if (xDir > 0)
            {
                animator.SetTrigger("DirectionRight");
            }
            else
            {
                animator.SetTrigger("DirectionLeft");
            }
        }
        else
        {
            if (yDir > 0)
            {
                animator.SetTrigger("DirectionUp");
            }
            else
            {
                animator.SetTrigger("DirectionDown");
            }
        }
    }
}

