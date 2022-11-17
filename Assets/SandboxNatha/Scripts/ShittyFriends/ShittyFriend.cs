using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyFriend : MonoBehaviour
{
 
    public enum ShittyFriendType {Attack,Heal}
    public ShittyFriendType type;
    private GameObject player;

    public int roomNumber;
    public int playerNumber;
    public bool addedToList=false;
    public bool attached = false;
    public float distanceFromPlayer = 1.5f;
    public float distanceBetweenFriends = 1.2f;

    private Vector3 targetPosition;
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    private GameObject previousShittyFriend;

    public float floatAmplitude = 0.05f;
    public float floatFrequency = .3f;
    private float delay;
    private Vector3 floatyness;

    public delegate void SpawnerCallback(int num);
    SpawnerCallback spawnerCallback;



    private void Update()
    {
        if (attached)
        {
            if (playerNumber == 0)
            {
                transform.LookAt(player.transform);
                targetPosition = player.transform.position - distanceFromPlayer * transform.forward;
            }
            else
            {
                previousShittyFriend = player.GetComponent<Player>().GetShittyFriend(playerNumber - 1);
                transform.LookAt(previousShittyFriend.transform);
                targetPosition = previousShittyFriend.transform.position -  distanceBetweenFriends * transform.forward;
            }
            delay = playerNumber * Mathf.PI / 3;
            floatyness = new Vector3(0, floatAmplitude * Mathf.Sin(2 * Mathf.PI* floatFrequency*Time.time-delay),0);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition + floatyness, ref velocity, smoothTime);

            if (transform.position.y < -0.2f)
            {
                transform.position = new Vector3(transform.position.x,-0.2f,transform.position.z);
            }
        }
    }

    public void InitiateProperties(int shittyFriendNumber, SpawnerCallback callback)
    {
        addedToList = true;
        roomNumber = shittyFriendNumber;
        spawnerCallback = callback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !attached)
        {
            player = other.gameObject;
            playerNumber = player.GetComponent<Player>().shittyFriendsList.Count;
            player.GetComponent<Player>().AddShittyFriend(gameObject);
            attached = true;
            spawnerCallback(roomNumber);
        }
    }
}
