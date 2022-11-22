using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDialogue : MonoBehaviour
{
    // add dialogue in dialogue
    public string[] dialogue;
    // add one npc name for all of dialogue or one npc name per dialogue line
    public string[] npcName;

    private bool played = false;
    public bool replayable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !played)
        {
            DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
            if (!replayable)
            {
                played = true;
            }
        }
    }

}
