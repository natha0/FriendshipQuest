using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RandySurprise : MonoBehaviour,IWeapon
{

    public float range = 4f;
    public LayerMask whatAreEnemies;

    public float _damage = 10;
    public float damage { 
        get { return _damage; } 
    }

    private float spawnTime;
    public float explosionDelay = 3;

    public GameObject kado,boom;


    private void Start()
    {
        spawnTime = Time.time;

        Invoke(nameof(Explode), explosionDelay);
    }
    public void PerformAttack()
    {
    }

    private void Explode()
    {
        boom.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(transform.position, range, whatAreEnemies);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Enemy"))
            {
                c.GetComponent<IDamageable>().Damage(_damage);
            }
        }
        kado.SetActive(false);
        Destroy(gameObject,4);
    }
}
