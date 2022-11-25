using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour,IWeapon
{
    public float _damage=1;
    public GameObject projectile;
    private AudioManager audioManager;
    public float damage
    {
        get { return _damage; }
    }

    private Transform playerTransform;

    public void PerformAttack()
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        proj.GetComponent<ProjectileProperties>().damage = _damage;
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        audioManager.Play("Piew",0.5f,1.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(playerTransform);
    }
}
