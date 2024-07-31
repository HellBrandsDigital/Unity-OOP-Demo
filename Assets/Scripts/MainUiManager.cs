using TMPro;
using UnityEngine;

public class MainUiManager : MonoBehaviour
{
    [SerializeField] private GameObject infoTextObject;
    [SerializeField] private GameObject startButtonObject;
    [SerializeField] private GameObject highScoreTextObject;
    [SerializeField] private GameObject currentScoreTextObject;
    private TextMeshProUGUI _currentScoreText;
    private TextMeshProUGUI _highScoreText;

    private TextMeshProUGUI _infoText;
    private GameObject[] _ingameUi;

    private GameObject[] _prePlayUi;

    private void Start()
    {
        _prePlayUi = new[] { infoTextObject, startButtonObject };
        _ingameUi = new[] { highScoreTextObject, currentScoreTextObject };

        _infoText = infoTextObject.GetComponent<TextMeshProUGUI>();
        _highScoreText = highScoreTextObject.GetComponent<TextMeshProUGUI>();
        _currentScoreText = currentScoreTextObject.GetComponent<TextMeshProUGUI>();

        foreach (var uiGameObject in _ingameUi)
        {
            uiGameObject.SetActive(false);
        }
    }

    public void OnStartPressed()
    {
        SwitchUi();
        UpdateScore(0);

        GameManager.Instance.StartGame();
    }

    public void OnGameEnded()
    {
        GameUtils.Print("UI OnGameEnded called!");
        SwitchUi();
        _infoText.text = "Game Over! \n You scored " + _currentScoreText.text[7..] +
                         " Points! \n Press Start to play again!";
    }

    public void UpdateScore(int currentScore)
    {
        _currentScoreText.text = "Score: " + currentScore;
    }

    public void UpdateHighScore(HighScoreDto highScoreDto)
    {
        _highScoreText.text = "High Score: " + highScoreDto.highScore + " by " + highScoreDto.playerName;
    }

    private void SwitchUi()
    {
        foreach (var ui in _prePlayUi)
        {
            ui.SetActive(!ui.activeSelf);
        }

        foreach (var ui in _ingameUi)
        {
            ui.SetActive(!ui.activeSelf);
        }
    }
}