using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour,IWeapon
{
    private Animator animator;
    public float _damage = 15;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        _damage = GetComponentInParent<Enemy>().damage;
    }

    public float damage
    {
        get { return _damage; }
    }

    public void PerformAttack()
    {

    }
}
