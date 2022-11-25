using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDialogue : MonoBehaviour
{
    public DialogueLine[] dialogue;

    private bool played = false;
    public bool replayable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !played)
        {
            DialogueSystem.Instance.AddNewDialogue(dialogue);
            if (!replayable)
            {
                played = true;
            }
        }
    }

}
