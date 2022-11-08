using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour,IWeapon
{
    public int damage = 3;
    private Animator animator;
    private bool hasAttacked=false;
    public float attackDelay = 2f;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        if (!hasAttacked)
        {
            animator.SetTrigger("Base_Attack");
            hasAttacked = true;
        }
        Invoke(nameof(resetAttack),attackDelay);
    }

    private void resetAttack()
    {
        hasAttacked = false;
    }

    public int GetWeaponDamage()
    {
        return damage; ;
    }
}
