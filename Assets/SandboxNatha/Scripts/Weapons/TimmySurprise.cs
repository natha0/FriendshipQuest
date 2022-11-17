using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmySurprise : MonoBehaviour,IWeapon
{
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
        //Kaboom
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
