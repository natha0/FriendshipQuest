using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public GameObject dialoguePanel;
    public List<string> dialogueLines = new();
    public string[] npcName;

    Button continueButton;
    TMP_Text dialogueText, nameText;
    int dialogueIndex;

    public delegate void DialogueEndCallback();
    private DialogueEndCallback dialogueEndCallback;

    public bool deactivateDialogues;


    private void Awake()
    {
        continueButton = dialoguePanel.transform.Find("Continue").gameObject.GetComponent<Button>();
        dialogueText = dialoguePanel.transform.Find("Dialogue Text").GetComponent<TMP_Text>();
        nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<TMP_Text>();

        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);

        if(Instance!=null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewDialogue(string[] lines, string[] npcName,DialogueEndCallback callback=null)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;
        dialogueEndCallback = callback;

        CreateDialogue();
    }

    public void CreateDialogue()
    {
        if (!deactivateDialogues)
        {
            dialogueText.text = dialogueLines[dialogueIndex];
            nameText.text = npcName.Length != 0 ? npcName[0] : "";
            dialoguePanel.SetActive(true);
        }
        else
        {
            dialogueEndCallback?.Invoke();
        }

    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count-1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
            if (npcName.Length > 1)
            {
                nameText.text = npcName[dialogueIndex];
            }
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueEndCallback?.Invoke();
        }
    }
}
