using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShittyFriendSpawnerModule
{
    public string name;
    [HideInInspector] public string type;
    public int maxNumber;
    [HideInInspector] public int number = 0;
    public GameObject shittyFriend;
    public float probabilityWeight;

    public void Start()
    {
        type = shittyFriend.GetComponent<ShittyFriend>().type;
    }


}
