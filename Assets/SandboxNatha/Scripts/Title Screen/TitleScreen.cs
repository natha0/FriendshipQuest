using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public static TitleScreen Instance { get; set; }

    public GameObject menuPanel, commandsPanel;

    Button startButton;
    Button commandsButton;
    Button commandsBackButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton = menuPanel.transform.Find("Start Button").gameObject.GetComponent<Button>();
        startButton.onClick.AddListener(delegate { StartGame(); });

        commandsButton = menuPanel.transform.Find("Commands Button").gameObject.GetComponent<Button>();
        commandsButton.onClick.AddListener(delegate { ShowCommands(); });

        commandsPanel.SetActive(false);
        commandsBackButton = commandsPanel.transform.Find("Back Button").gameObject.GetComponent<Button>();
        commandsBackButton.onClick.AddListener(delegate { HideCommands(); });

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void StartGame()
    {
        SceneManager.LoadSceneAsync("SandboxNatha");
    }

    void ShowCommands()
    {
        commandsPanel.SetActive(true);
    }

    void HideCommands()
    {
        commandsPanel.SetActive(false);
    }


}
