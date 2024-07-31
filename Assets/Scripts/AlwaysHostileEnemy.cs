using UnityEngine;

public class AlwaysHostileEnemy : BaseEnemy
{
    [SerializeField] private int score = 10;
    [SerializeField] private int health = 1;
    [SerializeField] private bool isHostile;

    private bool _hasMultiplier;

    // Start is called before the first frame update
    private void Start()
    {
        IsHostile = true;
        Health = health;
        ScorePoints = score;

        if (Random.value > 0.75f)
        {
            _hasMultiplier = true;
        }
    }

    // POLYMORPHISM
    protected override void ScoreMultiplier()
    {
        if (_hasMultiplier)
        {
            ScorePoints *= 2;
        }

        base.ScoreMultiplier();
    }
}
