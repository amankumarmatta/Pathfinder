using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel, menuPanel;
    public Button playButton, optionsButton, exitButton, backButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
        backButton.onClick.AddListener(BackButtonClicked);
    }

    private void BackButtonClicked()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    void PlayButtonClicked()
    {
        SceneManager.LoadScene("Sandbox");
    }

    void OptionsButtonClicked()
    {
        optionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    void ExitButtonClicked()
    {
        Application.Quit();
    }
}
