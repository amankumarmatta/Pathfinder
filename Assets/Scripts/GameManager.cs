using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public Button settings;
    public GameObject optionsPanel;

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
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;

        UpdateScoreText();
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
        optionsPanel.SetActive(true);
    }
}
