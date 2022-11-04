using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
    public float xPos, zPos;
    public int enemyNumber;
    public int enemyCount;

    private float xMin, xMax, zMin, zMax;

    // Start is called before the first frame update
    void Start()
    {
        float x = transform.position.x;
        xMin = x - transform.localScale.x / 2;
        xMax = x + transform.localScale.x / 2; 
        
        float z = transform.position.z;
        zMin = z - transform.localScale.z / 2;
        zMax = z + transform.localScale.z / 2;

        

    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < enemyNumber)
        { 
            xPos = Random.Range(xMin, xMax);
            zPos = Random.Range(zMin, zMax);
            Instantiate(enemyToSpawn, new Vector3(xPos, 0, zPos),Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

            enemyCount += 1;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);

        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnemyDrop());
        }
    }

}
