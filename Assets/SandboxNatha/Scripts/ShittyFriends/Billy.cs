using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billy : ShittyFriend,IShittyFriends
{
    private Player playerProperties;
    public float healthHealed = 10;

    private void Start()
    {
        playerProperties = GameObject.Find("Player").GetComponent<Player>();
    }

    public void UsePower()
    {
        playerProperties.HealPlayer(healthHealed);
    }
}
