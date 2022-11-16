using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Animator animator;
    private bool alreadyAttacked = false;
    public float attackDelay = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!alreadyAttacked)
            {
                animator.SetTrigger("baseAttack");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), attackDelay);
            }
            
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
