using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peon : Enemy
{
    public float damage = 5;
    private Animator animator;
    private NavMeshAgent agent;

    public override void Start()
    {
        base.Start();
        animator = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 1)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public override void PerformAttack()
    {
        animator.SetTrigger("Attack");
    }
}
