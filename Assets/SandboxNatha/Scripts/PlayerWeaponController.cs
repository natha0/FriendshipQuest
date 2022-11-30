using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private BoxCollider weaponCollider;
    public Animator animator;
    private bool alreadyAttacked = false;
    public float attackDelay = 0.2f;

    private AudioManager audioManager;

    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
        animator = gameObject.GetComponentInChildren<Animator>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!alreadyAttacked)
            {
                weaponCollider.enabled = false;
                animator.SetTrigger("baseAttack");
                alreadyAttacked = true;
                audioManager.Play("Sword Hit 1".ToString());
                Invoke(nameof(ResetAttack), attackDelay);
            }
            
        }
    }

    private void ResetAttack()
    {
        weaponCollider.enabled = false;
        alreadyAttacked = false;
    }

    public void ChangeSwordState()
    {
        weaponCollider.enabled = !weaponCollider.enabled;
    }

}
