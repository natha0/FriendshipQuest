using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karen : ShittyFriend,IShittyFriends
{
    private Transform playerTransform;
    public GameObject projectile;
    private AudioManager audioManager;

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    public bool UsePower()
    {
        audioManager.Play("KarenUse");
        Rigidbody rb = Instantiate(projectile, playerTransform.position, playerTransform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(playerTransform.forward * 32f, ForceMode.Impulse);
        bool PowerUsed = true;
        return PowerUsed;
    }
}
