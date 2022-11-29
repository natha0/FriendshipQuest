using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jimi : ShittyFriend,IShittyFriends
{

    private Player playerProperties;
    public float healthHealed = 10;

    public override void Start()
    {
        base.Start();
        playerProperties = GameObject.Find("Player").GetComponent<Player>();
    }

    public bool UsePower()
    {
        playerProperties.Heal(healthHealed);
        bool PowerUsed = true;
        return PowerUsed;
    }
}
