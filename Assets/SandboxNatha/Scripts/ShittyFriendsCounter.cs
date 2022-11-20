using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShittyFriendsCounter : MonoBehaviour
{
    public static ShittyFriendsCounter Instance { get; set; }
    public GameObject counterPanel;

    private TMP_Text Karen, Billy, Timmy;

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

        PlayerShittyFriendsManager manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShittyFriendsManager>();

        if (!manager.limitShittyFriendsNumber)
        {
            counterPanel.SetActive(false);
        }
        else
        {
            counterPanel.SetActive(true);
        }

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
}
