using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public float jumpDistance = 6;

    public bool sendToTarget;
    private Vector3 targetPosition;

    private Player player;

    private void Start()
    {
        Transform targetTransform = gameObject.transform.Find("Target");
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!sendToTarget)
            {
                other.transform.position = other.transform.position + transform.forward * jumpDistance;

            }
            else
            {
                targetPosition.y = other.transform.position.y;
                player.TeleportPlayer(targetPosition);
            }
        }
    }

}
