using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private int countToSpawn;
    [SerializeField] private GameObject[] trees;
    private float spawnXRange = 80f;
    private float spawnYRange = 80f;

    private void Start()
    {
        countToSpawn = Random.Range(150, 200);
        SpawnTrees(countToSpawn);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Tree").Length < countToSpawn)
        {
            SpawnTrees(countToSpawn - GameObject.FindGameObjectsWithTag("Tree").Length);
            Debug.Log("New tree has been spawned");
        }
    }

    private void SpawnTrees(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, trees.Length);
            Instantiate(trees[index], GenerateSpawnPositionXY(), Quaternion.identity);
        }
    }

    private Vector3 GenerateSpawnPositionXY()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnXRange, spawnXRange), Random.Range(-spawnYRange, spawnYRange), 0);
        return spawnPosition;
    }
}
