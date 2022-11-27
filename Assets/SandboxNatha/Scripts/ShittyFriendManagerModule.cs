using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShittyFriendManagerModule
{

    [HideInInspector] public int number = 0;
    public int maxNumber;
    public GameObject shittyFriend;
    [HideInInspector] public GameObject shittyFriendClone ;
    [HideInInspector] public string type => shittyFriend.GetComponent<ShittyFriend>().type;
    [HideInInspector] public ShittyFriend shittyFriendProperties => shittyFriendClone.GetComponent<ShittyFriend>();
    [HideInInspector] public int orderNumber = -1;

    public void Initialise()
    {
        orderNumber = -1;
    }


}
