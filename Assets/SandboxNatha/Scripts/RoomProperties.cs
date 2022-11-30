using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProperties : MonoBehaviour
{
    private Vector3 roomCenter;
    private Vector3 roomMin;
    private Vector3 roomMax;
    private CameraProperties cameraProperties;

    public DialogueLine[] onEnterDialogue;
    public bool onEnterReplayable;
    [HideInInspector] public bool onEnterPlayed;

    public DialogueLine[] onClearDialogue;
    public bool onClearReplayable;
    [HideInInspector] public bool onClearPlayed;

    public readonly List<BoxCollider> doorColliders=new();
    EnemySpawner spawner;

    public delegate void  OnDialogue();
    public OnDialogue onEnterDialoguePlayed;

    public delegate void SetRoomName(string roomName);
    public SetRoomName setRoomName;


    private bool letDoorsOpen => GodModeManager.Instance.letDoorsOpen;

    void Start()
    {
        Bounds bounds = GetComponent<BoxCollider>().bounds;
        roomMin = bounds.min;
        roomMax = bounds.max;
        roomCenter = bounds.center;

        cameraProperties = Camera.main.GetComponent<CameraProperties>();

        TeleportPlayer[] doorsTeleport = GetComponentsInChildren<TeleportPlayer>();

        foreach(TeleportPlayer door in doorsTeleport)
        {
            doorColliders.Add(door.gameObject.GetComponent<BoxCollider>());

        }

        spawner = GetComponentInChildren<EnemySpawner>();

        onEnterDialoguePlayed += () => onEnterPlayed = onEnterReplayable ?  true:false;
        GetComponent<EnemySpawner>().onRoomCleared += OnRoomCleared;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            setRoomName?.Invoke(name);

            cameraProperties.SetRoomBorders(roomCenter, roomMin, roomMax);
            if (spawner.enemiesInRoom > 0 && !letDoorsOpen)
            {
                DeactivateDoors();
            }

            if (!onEnterPlayed)
            {
                if (onEnterDialogue.Length > 0)
                {
                    DisplayOnEnterDialogue();
                    DialogueSystem.Instance.dialogueEndCallback += PlayEnterCallbacks;
                }
                else
                {
                    PlayEnterCallbacks();

                }
            }
        }
    }

    void PlayEnterCallbacks()
    {
        onEnterDialoguePlayed();
    }

    public void DisplayOnEnterDialogue()
    {
        DialogueSystem.Instance.AddNewDialogue(onEnterDialogue, PlayEnterCallbacks);
    }

    public void DisplayClearDialogue(DialogueSystem.DialogueEndCallback callback=null)
    {
        if (!onClearPlayed)
        {
            if (onClearDialogue.Length > 0)
            {
                DialogueSystem.Instance.AddNewDialogue(onClearDialogue,callback:callback);
            }
            else
            {
                callback?.Invoke();
            }


            if (!onClearReplayable)
            {
                onClearPlayed = true;
            }
        }
    }

    private void OnRoomCleared()
    {
        DisplayClearDialogue(ActivateDoors);
    }

    public void ActivateDoors()
    {
        foreach (BoxCollider door in doorColliders)
        {
            door.isTrigger = true;
        }
    }

    public void DeactivateDoors()
    {
        if (!letDoorsOpen)
        {
            foreach (BoxCollider door in doorColliders)
            {
                door.isTrigger = false;
            }
        }

    }
}
