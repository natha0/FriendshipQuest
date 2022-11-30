using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : Enemy
{
    public float attackDelay=2f;
    public float attackCooldown=2f;

    public GameObject bomba;
    public float bombaSpawnDistance = 5;
    public GameObject projectile;

    public float healthHealed = 10;

    public override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(UseRandomPower), attackDelay, attackCooldown);
    }


    private void UseRandomPower()
    {
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0: //Karen
                Rigidbody rb = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                break;
            case 1: //Jimi
                Heal(healthHealed);
                break;
            case 2: //Randy
                Instantiate(bomba, transform.position + bombaSpawnDistance * transform.forward, transform.rotation);
                break;
            case 3: //Billy

                break;
        }
    }
}
