using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenSpawner : MonoBehaviour
{

    public GameObject commandsPanel;
    Button commandsButton;
    Button commandsBackButton;


    public void StartGame()
    {
        SceneManager.LoadSceneAsync("SandboxNatha");
    }


    public void ShowCommands()
    {
        commandsPanel.SetActive(true);
    }

    public void HideCommands()
    {
        commandsPanel.SetActive(false);
    }
}
