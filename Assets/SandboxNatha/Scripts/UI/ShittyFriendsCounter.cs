using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShittyFriendsCounter : MonoBehaviour
{
    public static ShittyFriendsCounter Instance { get; set; }
    public GameObject canvas;

    public CounterModule[] modules;
    private CounterModule currentActiveModule;

    PlayerShittyFriendsManager manager;

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
        canvas.SetActive(true);


        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            CounterModule module = modules[i];
            module.panel = canvas.transform.Find("Panel " + i.ToString()).gameObject;
            module.Type = module.panel.transform.Find("Type").gameObject.GetComponent<TMP_Text>();
            module.Counter = module.panel.transform.Find("Counter").gameObject.GetComponent<TMP_Text>();
            module.panel.SetActive(false);
        }
        manager = GameObject.FindWithTag("Player").GetComponent<PlayerShittyFriendsManager>();
        manager.updateShittyFriends += UpdateCounter;

        UpdateCounter();
    }

    private void UpdateCounter()
    {
        int j = manager.shittyFriendsList.Length-1;
        foreach (ShittyFriendManagerModule SFmodule in manager.shittyFriendsList)
        {
            CounterModule module;
            if (SFmodule.orderNumber != -1)
            {
                module = modules[SFmodule.orderNumber];
                module.panel.SetActive(true);
            }
            else
            {
                module = modules[j];
                module.panel.SetActive(false);
                j--;
            }
            module.SetCounter(SFmodule.type, SFmodule.number);
        }
    }

    public void SetSelectedShittyFriend(string type, bool activated = true)
    {
        if (currentActiveModule != null)
        {
            currentActiveModule.Deselect();
            currentActiveModule = null;
        }

        if (activated)
        {
            currentActiveModule = Array.Find(modules, module => module.type == type);
            if (currentActiveModule != null)
            {
                currentActiveModule.SetSelected();
            }
            else
            {
                Debug.LogWarningFormat("Selected Shitty Friend type {} has not been implemented", type);
            }
        }
    }
}

[System.Serializable]
public class CounterModule
{
    public string type;
    public Transform ui;
    public GameObject panel;
    public TMP_Text Type, Counter;

    public void SetCounter(string SFtype, int count)
    {
        type = SFtype;
        Type.text = type;
        Counter.text = count.ToString();
    }

    public void SetSelected()
    {
        Type.outlineWidth = 0.3f;
        Counter.outlineWidth = 0.3f;
    }

    public void Deselect()
    {
        Type.outlineWidth = 0;
        Counter.outlineWidth = 0;
    }
}

