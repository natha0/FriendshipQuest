using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : Enemy
{

    delegate void Powers();
    Powers[] usePowers;

    public GameObject[] shittyFriends;

    public float attackDelay=2f;
    private float lastAttackTime;

    public GameObject bomba;
    public GameObject projectile;

    public float healthHealed = 10;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        usePowers = new Powers[3];
        usePowers[0] = UseKarenPower;
        usePowers[1] = UseBillyPower;
        usePowers[2] = UseTimmyPower;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastAttackTime) > attackDelay)
        {
            int i = Random.Range(0, shittyFriends.Length-1);
            usePowers[i]();
            lastAttackTime = Time.time;
        }
    }

    public void UseKarenPower()
    {
        Rigidbody rb = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
    }
    public void UseBillyPower()
    {
        Heal(healthHealed);
    }
    public void UseTimmyPower()
    {
        Instantiate(bomba, transform.position+4*transform.forward, transform.rotation);
    }
}
