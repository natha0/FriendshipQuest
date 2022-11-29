using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randy : ShittyFriend,IShittyFriends
{

    private Transform playerTransform;
    public GameObject bomba;

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }


    public bool UsePower()
    {

        Instantiate(bomba, playerTransform.position, playerTransform.rotation);
        bool PowerUsed = true;
        return PowerUsed;
    }


}
