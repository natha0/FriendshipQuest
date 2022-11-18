using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour,IWeapon
{
    
    public float _damage = 3;

    public float damage
    {
        get { return _damage; }
    }

    public void PerformAttack()
    {
        
    }
}
