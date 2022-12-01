using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randy : ShittyFriend,IShittyFriends
{

    private Transform playerTransform;
    public GameObject bomba;
    private AudioManager audioManager;

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }


    public bool UsePower()
    {
        audioManager.Play("Bomb");
        Instantiate(bomba, playerTransform.position, playerTransform.rotation);
        bool PowerUsed = true;
        return PowerUsed;
    }


}
