using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : Enemy
{
    public float attackDelay=2f;
    public float attackCooldown=2f;

    public GameObject bomba;
    public float bombaSpawnDistance = 5;
    public GameObject projectile;
    public float projectileSpeed = 20f;

    public float healthHealed = 10;

    private int phasesNumber=4;
    private int currentPhase;

    private bool isDialogue;

    public DialogueLine[] dialoguePhase0;
    public DialogueLine[] dialoguePhase1;
    public DialogueLine[] dialoguePhase2;
    public DialogueLine[] dialoguePhase3;
    public DialogueLine[] endDialogue;

    public override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(UseRandomPower), attackDelay, attackCooldown);
        isDialogue = false;

        DialogueSystem.Instance.DialogueStart += DialogueStart;
        DialogueSystem.Instance.DialogueEnd += DialogueEnd;
    }


    private void UseRandomPower()
    {
        if (!isDialogue)
        {
            int i = Random.Range(0, currentPhase);
            switch (i)
            {
                case 0: //Karen
                    Karen();
                    break;
                case 1: //Jimi
                    Jimi();
                    break;
                case 2: //Randy
                    Randy();
                    break;
                case 3: //Billy
                    Billy();
                    break;
            }
        }
    }

    private void Karen()
    {
        Rigidbody rb = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(projectileSpeed*transform.forward, ForceMode.Impulse);
    } 
    
    private void Jimi()
    {
        Heal(healthHealed);
    }

    private void Randy()
    {
        Instantiate(bomba, transform.position + bombaSpawnDistance * transform.forward, transform.rotation);
    }

    private void Billy()
    {

    }



    public override void KillSelf()
    {
        if (currentPhase < phasesNumber)
        {
            DialogueLine[] dialogue=null;
            switch (currentPhase)
            {
                case 0:
                    dialogue=dialoguePhase0;
                    break;
                case 1:
                    dialogue = dialoguePhase1;
                    break;
                case 2:
                    dialogue = dialoguePhase2;
                    break;
                case 3:
                    dialogue = dialoguePhase3;
                    break;
            }
            currentPhase++;
            health = maxHealth;
            UpdateHealthBar();
            if (dialogue != null)
            {
                DialogueSystem.Instance.AddNewDialogue(dialogue);
            }
        }
        else{
            spawnerCallback(number);
            DialogueSystem.Instance.AddNewDialogue(endDialogue);
            DialogueSystem.Instance.dialogueEndCallback += delegate { Destroy(gameObject); };
        }

    }

    private void DialogueStart()
    {
        isDialogue = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void DialogueEnd()
    {
        isDialogue = false;
    }

}
