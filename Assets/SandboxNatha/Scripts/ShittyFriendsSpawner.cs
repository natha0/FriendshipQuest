using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyFriendsSpawner : MonoBehaviour
{
    public GameObject ShittyFriends;
    public float xPos, zPos;
    public int shittyFriendsNumber;
    public float timeBetweenSpawn = 0.01f;
    private bool alreadySpawned;
    public float spawnDelay = 5;

    private List<GameObject> shittyFriendsInRoom = new();
    public List<GameObject> shittyFriendsType = new();

    public Vector3 deltaSpawn;
    public float wallWidth = 1;

    public bool randomSpawn;
    private float x, z;

    private bool isPlayerInside = false;

    private void Start()
    {
        x = transform.position.x;
        z = transform.position.z;
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Instantiate(shittyFriendsType[0], new Vector3(x + xPos, 0, z + zPos), Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(shittyFriendsType[1], new Vector3(x + xPos, 0, z + zPos), Quaternion.identity);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            foreach (GameObject shittyFriend in shittyFriendsInRoom)
            {
                shittyFriend.SetActive(true);
            }
        }
        else if (other.CompareTag("ShittyFriend"))
        {
            if (!isPlayerInside)
            {
                other.gameObject.SetActive(false);
            }
            ShittyFriend shittyFriend = other.gameObject.GetComponent<ShittyFriend>();
            if (!shittyFriend.addedToList && !shittyFriend.attached)
            {
                other.gameObject.GetComponent<ShittyFriend>().InitiateProperties(shittyFriendsInRoom.Count, RemoveShittyFriendFromList);
                shittyFriendsInRoom.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            foreach (GameObject enemy in shittyFriendsInRoom)
            {
                enemy.SetActive(false);
            }
        }
    }

    private void RemoveShittyFriendFromList(int number)
    {
        shittyFriendsInRoom.RemoveAt(number);
        for (int i = number; i < shittyFriendsInRoom.Count; i++)
        {
            shittyFriendsInRoom[i].GetComponent<ShittyFriend>().roomNumber = i;
        }
    }
}
