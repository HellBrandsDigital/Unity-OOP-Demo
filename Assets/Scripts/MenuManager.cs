using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnStartPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void OnNameEntered(string playerName)
    {
        GameManager.PlayerName = playerName;
    }
}
