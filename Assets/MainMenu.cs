using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);

        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    void PlayButtonClicked()
    {
        SceneManager.LoadScene("Sandbox");
    }

    void OptionsButtonClicked()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(!optionsPanel.activeSelf);
        }
    }

    void ExitButtonClicked()
    {
        Debug.Log("Exit button clicked. Display confirmation prompt.");
    }
}
