using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int maxCountOfEnemies = 20;
    int currentCountOfEnemies;
    private float spawnXRange = 75f;
    private float spawnYRange = 75f;
    public GameObject[] enemies;
    

    private void Update()
    {
        if (currentCountOfEnemies < maxCountOfEnemies)
        {
            SpawnEnemy();
        }
        currentCountOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

   private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomIndex], GenerateRandomPosition(), transform.rotation);
        Debug.Log("Enemy has been spawned");
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomPosX = Random.Range(-spawnXRange, spawnXRange);
        float randomPosY = Random.Range(-spawnYRange, spawnYRange);
        Vector3 randomSpawnPos = new Vector3(randomPosX, randomPosY, 0);
        return randomSpawnPos;
    }
}
