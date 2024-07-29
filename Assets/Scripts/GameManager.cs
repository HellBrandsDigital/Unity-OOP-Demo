using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static string PlayerName;

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
}
