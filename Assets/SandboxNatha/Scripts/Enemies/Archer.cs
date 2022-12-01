using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archer : Enemy
{
    private Transform playerTransform;
    public float detectPlayerRange = 2f;
    public float teleportCooldown = 2f;
    public float teleportRange = 5f;
    public int maxSearch = 1;
    public LayerMask whatIsGround;
    private bool isPlayerInRange;
    private Vector3 teleportPoint;
    private bool teleportPointSet;
    private bool canTeleport;

    private Animator animator;
    private NavMeshAgent agent;

    public float projectileSpeed = 20f;
    public GameObject projectile;
    private AudioManager audioManager;

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        canTeleport = true;
        animator = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if ((playerTransform.position - transform.position).magnitude < detectPlayerRange)
        {
            if (isPlayerInRange)
            {
                if (canTeleport)
                {
                    if (!teleportPointSet)
                    {
                        SearchTeleportPoint(transform.position, maxSearch);
                    }
                    else
                    {
                        Teleport();
                    }
                }
            }
            else
            {
                isPlayerInRange = true;
            }
        }
        else
        {
            isPlayerInRange = false;
        }

        if (agent.velocity.magnitude > 1)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

    }

    public void SearchTeleportPoint(Vector3 position,int maxSearch=1)
    {
        int i = 0;
        while(i < maxSearch &&!teleportPointSet)
        {
            i++;
            if (!teleportPointSet)
            {
                float theta = Random.Range(0, 2*Mathf.PI);
                Vector3 direction = new Vector3(Mathf.Cos(theta),0,Mathf.Sin(theta));

                teleportPoint = playerTransform.position + teleportRange * direction;
                if (Physics.Raycast(teleportPoint, -transform.up, 2f, whatIsGround) && isInRoom(teleportPoint))
                {
                    teleportPointSet = true;
                }
            }
        }
    }

    public void Teleport()
    {
        transform.position = teleportPoint;
        teleportPointSet = false;
        canTeleport = false;
        Invoke(nameof(ResetTeleport), teleportCooldown);
    }

    void ResetTeleport()
    {
        canTeleport = true;
    }

    public override void PerformAttack()
    {
        animator.SetTrigger("Attack");

        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        proj.GetComponent<ProjectileProperties>().damage = damage;
        rb.AddForce(projectileSpeed * transform.forward, ForceMode.Impulse);
        audioManager.Play("Piew", 0.8f, 1.2f);
    }

}
