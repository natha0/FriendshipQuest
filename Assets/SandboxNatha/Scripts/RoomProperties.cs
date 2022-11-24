using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProperties : MonoBehaviour
{
    private Vector3 roomCenter;
    private Vector3 roomMin;
    private Vector3 roomMax;
    private CameraProperties cameraProperties;

    public string[] onEnterDialogue;
    public string[] onEnterNpcName;
    public bool onEnterReplayable;
    [HideInInspector] public bool onEnterPlayed;

    public string[] onClearDialogue;
    public string[] onClearNpcName;
    public bool onClearReplayable;
    [HideInInspector] public bool onClearPlayed;

    public readonly List<BoxCollider> doorColliders=new();
    EnemySpawner spawner;

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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraProperties.SetRoomBorders(roomCenter, roomMin, roomMax);
            if (spawner.enemiesInRoom > 0 && !letDoorsOpen)
            {
                DeactivateDoors();
            }
        }
    }

    public void DisplayOnEnterDialogue(DialogueSystem.DialogueEndCallback callback=null)
    {
        if (!onEnterPlayed)
        {
            if (onEnterDialogue.Length > 0)
            {
                DialogueSystem.Instance.AddNewDialogue(onEnterDialogue, onEnterNpcName, callback);
            }
            else
            {
                callback?.Invoke();
            }
            if (!onEnterReplayable)
            {
                onEnterPlayed = true;
            }
        }
    }

    public void DisplayClearDialogue(DialogueSystem.DialogueEndCallback callback=null)
    {
        if (!onClearPlayed)
        {
            if (onClearDialogue.Length > 0)
            {
                DialogueSystem.Instance.AddNewDialogue(onClearDialogue, onClearNpcName, callback);
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
