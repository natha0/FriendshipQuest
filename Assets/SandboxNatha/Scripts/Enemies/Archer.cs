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
    public Vector3 previousPosition;


    void Start()
    {
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
        for(int i = 0; i < maxSearch; i++)
        {
            if (!teleportPointSet)
            {
                float randomZ = Random.Range(-teleportRange, teleportRange);
                float randomX = Random.Range(-teleportRange, teleportRange);

                teleportPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);
                if (Physics.Raycast(teleportPoint, -transform.up, 2f, whatIsGround) && (playerTransform.position- teleportPoint).magnitude>detectPlayerRange)
                {
                    teleportPointSet = true;
                }
            }
        }
    }

    public void Teleport()
    {
        previousPosition = transform.position;
        transform.position = teleportPoint;
        teleportPointSet = false;
    }
}
