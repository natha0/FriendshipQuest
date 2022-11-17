using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmy : ShittyFriend,IShittyFriends
{

    private Transform playerTransform;
    public GameObject bomba;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }


    public void UsePower()
    {
        Instantiate(bomba, playerTransform.position, playerTransform.rotation);
    }


}
