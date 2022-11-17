using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IWeapon 
{
    //int GetWeaponDamage();

    float damage { get; }

    void PerformAttack();
}
