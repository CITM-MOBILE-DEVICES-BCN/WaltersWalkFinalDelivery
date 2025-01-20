using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerRuin : MonoBehaviour
{
    [SerializeField] private EnemyLevelData enemyLevelData;

    private void Start()
    {
        if (enemyLevelData == null)
        {
            Debug.LogError("Enemy Level Data is not assigned in the inspector");
            return;
        }

        SpawnAllEnemies();
    }

    public void SpawnAllEnemies()
    {
        foreach (var spawnPoint in enemyLevelData.enemySpawnPoints)
        {
            EnemyBuilder.BuildEnemy<EnemyRuinSeeker>(spawnPoint.position, spawnPoint.enemyType);
        }
    }

    private void SpawnEnemy(Vector3 position, string enemyType)
    {
        var enemy = EnemyBuilder.BuildEnemy<EnemyRuinSeeker>(position, enemyType);

        if (enemy == null)
        {
            Debug.LogWarning($"Failed to spawn enemy of type {enemyType} at position {position}");
            return;
        }
    }

    public void DeleteAllEnemies()
    {
        var enemies = FindObjectsOfType<EnemyRuinSeeker>();

        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
