using UnityEngine;

// INHERITANCE
public class WeakEnemy : BaseEnemy
{
    [SerializeField] private int score = 10;
    [SerializeField] private int health = 1;
    [SerializeField] private bool isHostile;

    // Start is called before the first frame update
    private void Start()
    {
        isHostile = Random.value > 0.75f;
        IsHostile = isHostile;
        Health = health;
        ScorePoints = score;
        SetSpriteColor();
    }
}
