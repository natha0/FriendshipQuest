using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    private Transform playerTransform;
    public float detectPlayerRange = 2f;
    public float teleportDelay = 2f;
    public float teleportRange = 5f;
    public int maxSearch = 1;
    public LayerMask whatIsGround;
    private bool isPlayerInRange;
    private float inRangeTime;
    private Vector3 teleportPoint;
    private bool teleportPointSet;

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if ((playerTransform.position - transform.position).magnitude < detectPlayerRange)
        {
            if (isPlayerInRange)
            {
                if ((Time.time - inRangeTime) > teleportDelay)
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
                inRangeTime = Time.time;
            }
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    public void SearchTeleportPoint(Vector3 position,int maxSearch=1)
    {
        int i = 0;
        while(i < maxSearch &&!teleportPointSet)
        {
            if (!teleportPointSet)
            {
                /*                Vector3 position=playerTransform.position;
                float randomZ = Random.Range(-teleportRange, teleportRange);
                float randomX = Random.Range(-teleportRange, teleportRange);*/

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
    }
}
