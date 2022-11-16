using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billy : ShittyFriend,IShittyFriends
{
    private Player player;
    public float healthHealed = 10;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void UsePower()
    {
        player.HealPlayer(healthHealed);
    }
}
