using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karen : ShittyFriend,IShittyFriends
{
    private Transform playerTransform;
    public GameObject projectile;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    public bool UsePower()
    {
        Rigidbody rb = Instantiate(projectile, playerTransform.position, playerTransform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(playerTransform.forward * 32f, ForceMode.Impulse);
        bool PowerUsed = true;
        return PowerUsed;
    }
}
