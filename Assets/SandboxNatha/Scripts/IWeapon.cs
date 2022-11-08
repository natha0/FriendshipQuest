using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IWeapon 
{
    int GetWeaponDamage();

    void PerformAttack();
}
