using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverPanel;
    Button titleScreenButton;

    // Start is called before the first frame update
    void Start()
    {
        titleScreenButton = gameOverPanel.transform.Find("TitleScreen Button").gameObject.GetComponent<Button>();
        titleScreenButton.onClick.AddListener(delegate { ReturnToTitleScreen(); });
    }


    void ReturnToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }


}
