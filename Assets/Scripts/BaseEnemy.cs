using UnityEngine;
using Random = UnityEngine.Random;

// ABSTRACTION
public abstract class BaseEnemy : MonoBehaviour
{
    public delegate void EnemyEventHandler(int score, GameObject enemy);

    private SpriteRenderer _spriteRenderer;

    protected int Health { get; set; }
    protected bool IsHostile { get; set; }
    protected int ScorePoints { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        TakeDamage(1);
    }

    public event EnemyEventHandler OnEnemyDied;

    protected virtual void ScoreMultiplier()
    {
        // Do nothing
    }


    private void TakeDamage(int damage)
    {
        GameUtils.Print($"{gameObject.name} took {damage} damage");
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (IsHostile)
        {
            ScorePoints *= -1;
        }

        OnEnemyDied?.Invoke(ScorePoints, gameObject);
    }

    protected void SetSpriteColor()
    {
        // Set the sprite color to red if the enemy is hostile
        // Otherwise choose a random color
        _spriteRenderer.color = IsHostile ? Color.red : Random.ColorHSV();
    }
}
