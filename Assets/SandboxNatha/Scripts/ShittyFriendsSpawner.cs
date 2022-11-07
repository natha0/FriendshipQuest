using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyFriendsSpawner : MonoBehaviour
{
    public GameObject ShittyFriends;
    private float xPos, zPos;
    public int shittyFriendsNumber;
    public float timeBetweenSpawn = 0.01f;
    private int spawnCount;
    private bool alreadySpawned;
    public float spawnDelay = 5;

    private List<GameObject> shittyFriendsInRoom = new();

    public Vector3 deltaSpawn;
    public float wallWidth = 1;

    private float x, z;
    private float dx, dz;
    private float xMin, xMax, zMin, zMax;
    public bool randomSpawn;

    private bool isPlayerInside = false;

    // Start is called before the first frame update
    void Start()
    {
        alreadySpawned = false;
        x = transform.position.x;
        z = transform.position.z;

        dx = Mathf.Clamp(deltaSpawn.x, 0, transform.localScale.x / 2 - wallWidth);
        dz = Mathf.Clamp(deltaSpawn.z, 0, transform.localScale.z / 2 - wallWidth);

        xMin = x - dx;
        xMax = x + dx;
        zMin = z - dz;
        zMax = z + dz;
    }

    IEnumerator EnemyDrop()
    {
        while (spawnCount < shittyFriendsNumber)
        {
            xPos = Random.Range(xMin, xMax);
            zPos = Random.Range(zMin, zMax);
            Instantiate(ShittyFriends, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawn);
            spawnCount++;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (!alreadySpawned)
            {
                if (randomSpawn)
                {
                    StartCoroutine(EnemyDrop());
                    alreadySpawned = true;
                }
            }

            foreach (GameObject enemy in shittyFriendsInRoom)
            {
                enemy.SetActive(true);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (!isPlayerInside)
            {
                other.gameObject.SetActive(false);
            }

            if (!other.gameObject.GetComponent<EnemyProperties>().addedToList)
            {
                other.gameObject.GetComponent<EnemyProperties>().InitiateProperties(shittyFriendsInRoom.Count, RemoveEnnemyFromList);
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

    private void RemoveEnnemyFromList(int number)
    {
        shittyFriendsInRoom.RemoveAt(number);
        for (int i = number; i < shittyFriendsInRoom.Count; i++)
        {
            shittyFriendsInRoom[i].GetComponent<EnemyProperties>().number = i;
        }
    }
}
