using System;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;

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

    public void SaveHighScore(string playerName, int highScore)
    {
        var highScoreDto = new HighScoreDto(playerName, highScore);
        GameUtils.SaveData(highScoreDto);
    }

    public HighScoreDto LoadHighScore()
    {
        return GameUtils.LoadData<HighScoreDto>();
    }
}

[Serializable]
public class HighScoreDto
{
    public string playerName;
    public int highScore;

    public HighScoreDto(string playerName, int highScore)
    {
        this.playerName = playerName;
        this.highScore = highScore;
    }
}
