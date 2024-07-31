using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const float ScreenWidth = 8f;
    private const float ScreenHeight = 4f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private int maxEnemies = 10;

    private List<GameObject> _enemies;
    private bool _shouldSpawnEnemies;

    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _shouldSpawnEnemies = false;
        _enemies = new List<GameObject>();

        DestroyerLineScript.OnEnemyDestroyed += DeleteEnemy;
    }

    private void Update()
    {
        switch (_shouldSpawnEnemies)
        {
            // Handle spawning based on the flag
            case true when _spawnCoroutine == null:
                StartSpawning();
                break;
            case false when _spawnCoroutine != null:
                StopSpawning();
                break;
        }
    }

    public void SetSpawnStatus(bool shouldSpawn)
    {
        _shouldSpawnEnemies = shouldSpawn;
    }

    private void StartSpawning()
    {
        GameUtils.Print("Start Spawning Enemies");
        _enemies = new List<GameObject>();
        _spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void StopSpawning()
    {
        GameUtils.Print("Stop Spawning Enemies");
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        foreach (var enemy in _enemies.Where(enemy => enemy))
        {
            Destroy(enemy);
        }

        _enemies.Clear();
    }

    private IEnumerator SpawnEnemies()
    {
        while (_enemies.Count < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        var x = Random.Range(-ScreenWidth, ScreenWidth);
        var y = Random.Range(-ScreenHeight, ScreenHeight);
        var randomSpawnPoint = new Vector2(x, y);

        var randomEnemy = Random.Range(0, enemyPrefabs.Length);
        var enemyObject = Instantiate(enemyPrefabs[randomEnemy], randomSpawnPoint, Quaternion.identity);

        var enemy = enemyObject.GetComponent<BaseEnemy>();
        if (enemy) enemy.OnEnemyDied += OnEnemyDied;

        _enemies.Add(enemyObject);
        AddForceToEnemy(enemyObject);
    }

    private void AddForceToEnemy(GameObject enemy)
    {
        var enemyRigidbody = enemy.GetComponent<Rigidbody2D>();

        var randomForce = Random.insideUnitCircle.normalized * Random.Range(10, 200);

        enemyRigidbody.AddForce(randomForce);
    }

    private void OnEnemyDied(int score, GameObject enemy)
    {
        GameUtils.Print("Enemy Died!, Score: " + score);
        GameManager.Instance.UpdateScore(score);
        DeleteEnemy(enemy);
    }

    private void DeleteEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
        Destroy(enemy);
    }
}
