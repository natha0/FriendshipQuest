using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour,IWeapon
{
    public float _damage=1;
    public GameObject projectile;
    public float damage
    {
        get { return _damage; }
    }

    public void PerformAttack()
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        proj.GetComponent<ProjectileProperties>().damage = _damage;
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
