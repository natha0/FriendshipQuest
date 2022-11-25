using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyFriend : MonoBehaviour
{
    public string type=null;
    private GameObject playerObject;
    private PlayerShittyFriendsManager shittyFriendsManager;

    [HideInInspector] public int roomNumber;
    [HideInInspector] public int playerNumber;
    [HideInInspector] public bool addedToList=false;
    [HideInInspector] public bool attached = false;
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

    public virtual void Update()
    {
        if (attached)
        {
            if (playerNumber == 0)
            {
                transform.LookAt(playerObject.transform);
                targetPosition = playerObject.transform.position - distanceFromPlayer * transform.forward;
            }
            else
            {
                previousShittyFriend = shittyFriendsManager.GetShittyFriend(playerNumber - 1);
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
            playerObject = other.gameObject;
            shittyFriendsManager = playerObject.GetComponent<PlayerShittyFriendsManager>();

            spawnerCallback(roomNumber);
            if (shittyFriendsManager.AddShittyFriend(gameObject))
            {
                Destroy(gameObject);
            }
            else
            {
                playerNumber = shittyFriendsManager.shittyFriendsTypes.IndexOf(type);
                attached = true;
            }
            
        }
    }
}
