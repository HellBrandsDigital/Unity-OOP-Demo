using UnityEngine;

public class DestroyerLineScript : MonoBehaviour
{
    public delegate void EnemyDestroyed(GameObject enemy);

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnEnemyDestroyed?.Invoke(other.gameObject);
    }

    public static event EnemyDestroyed OnEnemyDestroyed;
}
