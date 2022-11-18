using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessAnimatorScipt : MonoBehaviour
{

    private PlayerWeaponController playerWeaponController;
    // Start is called before the first frame update
    void Start()
    {
        playerWeaponController = gameObject.GetComponentInParent<PlayerWeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSwordState()
    {
        playerWeaponController.ChangeSwordState();
    }
}
