using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeManager : MonoBehaviour
{
    public static GodModeManager Instance { get; set; }

    public bool deactivateDialogues, letDoorsOpen,deactivateGameOver;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


}
