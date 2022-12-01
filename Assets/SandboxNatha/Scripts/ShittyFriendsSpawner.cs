using System.Collections.Generic;
using UnityEngine;

public class ShittyFriendsSpawner : MonoBehaviour
{
    private float xPos, zPos;
    private bool alreadySpawning;
    public bool canSpawn = true;
    public int initialSpawnNumber = 5;
    public float spawnDelay = 10;
    public float spawnCooldown = 5;

    private RoomProperties room;
    public ShittyFriendSpawnerModule[] shittyFriendsList;
    private List<GameObject> shittyFriendsInRoom = new();

    private float xmin, xmax, zmin, zmax;

    private bool isPlayerInside = false;

    private bool CanSpawnShittyFriends => GodModeManager.Instance.CanSpawnShittyFriends;

    private bool AllSpawned { get { 
            foreach (ShittyFriendSpawnerModule module in shittyFriendsList) 
            { if (module.number < module.maxNumber) { return false; } } 
            return true; } }

    private void Start()
    {
        Bounds bounds = GetComponent<BoxCollider>().bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        xmin = min.x;
        zmin = min.z;
        xmax = max.x;
        zmax = max.z;


        alreadySpawning = false;

        room = GetComponent<RoomProperties>();
        room.onEnterDialoguePlayed += StartSpawn;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().gameOver += StopSpawn;
    }

    private void Update()
    {
        if (isPlayerInside && CanSpawnShittyFriends)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SpawnShittyFriend(module: shittyFriendsList[0]);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                SpawnShittyFriend(module: shittyFriendsList[1]);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                SpawnShittyFriend(module: shittyFriendsList[2]);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                SpawnShittyFriend(module: shittyFriendsList[3]);
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
            foreach (GameObject shittyFriend in shittyFriendsInRoom)
            {
                shittyFriend.SetActive(false);
            }
        }
    }

    private void RemoveShittyFriendFromList(int number)
    {
        string type = shittyFriendsInRoom[number].GetComponent<ShittyFriend>().type;
        ShittyFriendSpawnerModule module = System.Array.Find(shittyFriendsList, mod => mod.type == type);
        module.number--;
        shittyFriendsInRoom.RemoveAt(number);
        for (int i = number; i < shittyFriendsInRoom.Count; i++)
        {
            shittyFriendsInRoom[i].GetComponent<ShittyFriend>().roomNumber = i;
        }
    }

    private void StartSpawn()
    {
        if (!alreadySpawning)
        {
            for (int i = 0; i < initialSpawnNumber; i++)
            {
                SpawnRandomShittyFriend();
            }
            alreadySpawning = true;

            InvokeRepeating(nameof(SpawnRandomShittyFriend), spawnDelay, spawnCooldown);
        }
    }

    void SpawnRandomShittyFriend()
    {
        if (isPlayerInside && !AllSpawned)
        {
            ShittyFriendSpawnerModule module = shittyFriendsList[ChooseRandomPossibleShittyFriend()];
            int i = 0;
            while (module.number == module.maxNumber && i != -1)
            {
                i = ChooseRandomPossibleShittyFriend();
                if (i != -1)
                {
                    module = shittyFriendsList[i];
                }
            }
            if (module.number < module.maxNumber && i!=-1)
            {
                float x, z;
                x = Random.Range(xmin, xmax);
                z = Random.Range(zmin, zmax);

                Vector3 position = new(x, 0, z);

                Instantiate(module.shittyFriend, position, Quaternion.identity);
                module.number++;
            }
        }
    }

    void SpawnShittyFriend(ShittyFriendSpawnerModule module)
    {
        Instantiate(module.shittyFriend, transform.position- new Vector3(8,transform.position.y,3), Quaternion.identity);
        module.number++;
    }
        

    int ChooseRandomPossibleShittyFriend()
    {
        float totalWeight = 0;
        foreach(ShittyFriendSpawnerModule module in shittyFriendsList)
        {
            if (module.number != module.maxNumber)
            {
                totalWeight += module.spawnWeight;
            }
        }
        if (totalWeight > 0 || AllSpawned)
        {
            float currentTotalWeight = 0;
            float weight;
            float result = Random.Range(0f, totalWeight);

            for (int i = 0; i < shittyFriendsList.Length; i++)
            {
                ShittyFriendSpawnerModule module = shittyFriendsList[i];
                if (module.number != module.maxNumber)
                {
                    weight = module.spawnWeight;
                    if (currentTotalWeight <= result && result < currentTotalWeight + weight)
                    {
                        return i;
                    }
                    currentTotalWeight += weight;
                }
            }
        }

        return -1;
    }

    private void StopSpawn()
    {
        CancelInvoke(nameof(SpawnRandomShittyFriend));
    }
}

    [System.Serializable]
    public class ShittyFriendSpawnerModule
    {
        public string name;
        public GameObject shittyFriend;
        [HideInInspector] public string type => shittyFriend.GetComponent<ShittyFriend>().type;
        public int number = 0;
        public int maxNumber;
        public float spawnWeight;
    }
