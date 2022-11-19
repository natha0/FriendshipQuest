using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSurprise : MonoBehaviour,IWeapon
{

    public float range = 4f;
    public LayerMask whatAreEnemies;

    public float _damage = 10;
    public float damage { 
        get { return _damage; } 
    }

    private float spawnTime;
    public float explosionDelay = 3;


    private void Start()
    {
        spawnTime = Time.time;
    }
    public void PerformAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, whatAreEnemies);
        foreach(Collider c in colliders)
        {
            if (c.CompareTag("Player"))
            {
                c.GetComponent<IDamageable>().Damage(_damage);
            }
        }
    }

    void Update()
    {
        if ((Time.time-spawnTime) > explosionDelay)
        {
            PerformAttack();
            Destroy(gameObject);
        }  
    }
}
