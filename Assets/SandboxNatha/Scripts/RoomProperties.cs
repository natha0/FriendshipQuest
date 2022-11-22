using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProperties : MonoBehaviour
{
    private Vector3 roomCenter;
    private Vector3 roomMin;
    private Vector3 roomMax;
    private CameraProperties cameraProperties;

    public string[] onEnterDialogue = new string[1];
    public string[] onEnterNpcName = new string[1];
    public bool onEnterReplayable;
    [HideInInspector] public bool onEnterPlayed;

    public string[] onClearDialogue;
    public string[] onClearNpcName;
    public bool onClearReplayable;
    [HideInInspector] public bool onClearPlayed;

    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = GetComponent<BoxCollider>().bounds;
        roomMin = bounds.min;
        roomMax = bounds.max;
        roomCenter = bounds.center;

        cameraProperties = Camera.main.GetComponent<CameraProperties>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraProperties.SetRoomBorders(roomCenter, roomMin, roomMax);
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
}
