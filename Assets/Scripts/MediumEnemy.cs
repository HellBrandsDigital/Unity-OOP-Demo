using UnityEngine;

public class MediumEnemy : BaseEnemy
{
    [SerializeField] private int score = 20;
    [SerializeField] private int health = 2;
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