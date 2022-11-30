using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour,IWeapon
{
    public float _damage=10;
    public float damage
    {
        get { return _damage; }
    }

    void Start()
    {
        _damage = GetComponentInParent<Enemy>().damage;
    }

    public void PerformAttack()
    {

    }

}
