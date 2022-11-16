using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour,IWeapon
{
    private Animator animator;
    public float _damage = 3;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public float damage
    {
        get { return _damage; }
    }

    public void PerformAttack()
    {
        animator.SetTrigger("baseAttack");
    }
}
