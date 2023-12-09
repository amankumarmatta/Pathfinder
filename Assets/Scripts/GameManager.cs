using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public Button settings, options,back, exit;
    public GameObject optionsPanel, settingsPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
        settings.onClick.AddListener(SettingsBtn);
        options.onClick.AddListener(OptionsButton);
        back.onClick.AddListener(BackButtonClicked);
        exit.onClick.AddListener(ExitButton);
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;

        UpdateScoreText();

        if (score >= 10)
        {
            SceneManager.LoadScene("Win");
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void SettingsBtn()
    {
        Time.timeScale = 0f;
        settings.gameObject.SetActive(false);
        settingsPanel.SetActive(true);
    }

    void OptionsButton()
    {
        settingsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    private void BackButtonClicked()
    {
        optionsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    void ExitButton()
    {
        Application.Quit();
    }
}
