using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileProperties : MonoBehaviour
{
    // Start is called before the first frame update
    public float range = 5f;
    private Vector3 initialPosition;

    public float lifeTime = 3f;
    private float spawnTime;

    public float playerDamage = 1;

    void Start()
    {
        initialPosition = transform.position;
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - initialPosition).magnitude >= range || Time.time - spawnTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
