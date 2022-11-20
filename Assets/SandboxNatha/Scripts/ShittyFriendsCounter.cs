using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShittyFriendsCounter : MonoBehaviour
{
    public static ShittyFriendsCounter Instance { get; set; }
    public GameObject counterPanel;

    private TMP_Text Karen, Billy, Timmy;
    private TMP_Text selectedType=null;

    int i;
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

        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShittyFriendsManager>();


        counterPanel.SetActive(true);
        Karen = counterPanel.transform.Find("Karen").GetComponent<TMP_Text>();
        Billy = counterPanel.transform.Find("Billy").GetComponent<TMP_Text>();
        Timmy = counterPanel.transform.Find("Timmy").GetComponent<TMP_Text>();
    }

    public void SetShittyFriendCount(string type,int number)
    {
        switch (type)
        {
            case "Karen":
                Karen.SetText("Karen<br>{0}", number);
                break;
            case "Billy":
                Billy.SetText("Billy<br>{0}", number);
                break;
            case "Timmy":
                Timmy.SetText("Timmy<br>{0}", number);
                break;
        }
    }

    public void SetSelectedShittyFriend(string type,bool activated=true)
    {
        if (activated)
        {
            TMP_Text textToChange = null;
            switch (type)
            {
                case "Karen":
                    textToChange = Karen;
                    break;
                case "Billy":
                    textToChange = Billy;
                    break;
                case "Timmy":
                    textToChange = Timmy;
                    break;
            }
            if (textToChange != null)
            {
                if (selectedType != null)
                {
                    selectedType.outlineWidth = 0;
                }
                textToChange.outlineWidth = 0.3f;
                selectedType = textToChange;
            }
            else
            {
                Debug.LogWarningFormat("Selected Shitty Friend type {} has not been implemented",type);
            }
        }
        else
        {
            selectedType.outlineWidth = 0;
        }
    }
}
