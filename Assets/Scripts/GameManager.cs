using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private string playerName;
    private int _currentScore;
    private int _highScore;

    private MainUiManager _mainUiManager;
    private SpawnManager _spawnManager;

    // ENCAPSULATION
    public string PlayerName
    {
        get => playerName;
        set => playerName = value.Length > 10 ? value[..10] : value;
    }


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StartGame()
    {
        GameUtils.Print("Game Started!");
        SetVariables();
        UpdateHighScore();
        _currentScore = 0;
        _spawnManager.SetSpawnStatus(true);
        StartCoroutine(StartGameCoroutine(10));
    }

    private IEnumerator StartGameCoroutine(int gameDurationInSeconds)
    {
        yield return new WaitForSeconds(gameDurationInSeconds);
        EndGame();
    }

    private void EndGame()
    {
        GameUtils.Print("Game Ended!");
        _spawnManager.SetSpawnStatus(false);

        if (_currentScore > _highScore)
        {
            GameUtils.Print("New High Score!");
            PersistenceManager.Instance.SaveHighScore(PlayerName, _currentScore);
            _highScore = _currentScore;
            var highScoreDto = new HighScoreDto(PlayerName, _currentScore);
            _mainUiManager.UpdateHighScore(highScoreDto);
        }

        _mainUiManager.OnGameEnded();
    }

    public void UpdateScore(int score)
    {
        _currentScore += score;
        _mainUiManager.UpdateScore(_currentScore);
    }

    private void SetVariables()
    {
        try
        {
            _mainUiManager = FindObjectOfType<MainUiManager>();
            _spawnManager = FindObjectOfType<SpawnManager>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void UpdateHighScore()
    {
        try
        {
            var highScoreDto = PersistenceManager.Instance.LoadHighScore();
            GameUtils.Print("High Score: " + highScoreDto.highScore + " by " + highScoreDto.playerName);
            _highScore = highScoreDto.highScore;
            _mainUiManager.UpdateHighScore(highScoreDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var highScoreDto = new HighScoreDto("No One", 0);
            PersistenceManager.Instance.SaveHighScore("No One", 0);
            _highScore = highScoreDto.highScore;
            _mainUiManager.UpdateHighScore(highScoreDto);
        }
    }
}
