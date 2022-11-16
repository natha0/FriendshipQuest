using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timmy : ShittyFriend,IShittyFriends
{

    private Transform playerTransform;
    public GameObject bomba;

    private void Awake()
    {
        
    }


    public void UsePower()
    {
        Instantiate(bomba, playerTransform.position, playerTransform.rotation);
    }


}
