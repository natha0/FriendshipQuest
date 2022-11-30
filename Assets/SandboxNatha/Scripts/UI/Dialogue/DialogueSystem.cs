using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public GameObject dialoguePanel;

    public DialogueLine[] dialogueLines;
    private DialogueLine currentLine;

    Button continueButton;
    TMP_Text dialogueText, nameText;
    int dialogueIndex;

    public delegate void DialogueEndCallback();
    public DialogueEndCallback dialogueEndCallback;

    private bool deactivateDialogues => GodModeManager.Instance.deactivateDialogues;


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

    public void AddNewDialogue(DialogueLine[] dialogue, DialogueEndCallback callback = null)
    {
        dialogueIndex = 0;
        dialogueLines = dialogue;

        if (callback != null)
        {
            dialogueEndCallback += callback;
        }

        if (dialogue!=null)
        {
            CreateDialogue();
        }

    }

    public void CreateDialogue()
    {


        if (!deactivateDialogues)
        {
            currentLine = dialogueLines[dialogueIndex];
            dialogueText.text = GetLineText(currentLine);
            nameText.text = dialogueLines[dialogueIndex].npcName;
            dialoguePanel.SetActive(true);
        }
        else
        {
            dialogueEndCallback?.Invoke();
            dialogueEndCallback = null;
        }
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Length-1)
        {
            dialogueIndex++;
            currentLine = dialogueLines[dialogueIndex];
            dialogueText.text = GetLineText(currentLine);
            nameText.text = dialogueLines[dialogueIndex].npcName;
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueEndCallback?.Invoke();
            dialogueEndCallback = null;
        }
    }

    private string GetLineText(DialogueLine dialogueLine)
    {
        string line = "";
        for (int i = 0; i < dialogueLine.Lines.Length; i++)
        {
            line += dialogueLine.Lines[i];
            if (i != dialogueLine.Lines.Length - 1)
            {
                line += "<br>";
            }
        }
        return line;
    }

}
