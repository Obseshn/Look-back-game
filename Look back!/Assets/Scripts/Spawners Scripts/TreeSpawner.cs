using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private int countToSpawn;
    [SerializeField] private GameObject[] trees;

    private readonly string TAG_Tree = "Tree";


    private readonly float spawnXRange = 70f;
    private readonly float spawnYRange = 70f;

    private void Start()
    {
        countToSpawn = Random.Range(150, 200);
        SpawnTrees(countToSpawn);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag(TAG_Tree).Length < countToSpawn)
        {
            SpawnTrees(countToSpawn - GameObject.FindGameObjectsWithTag(TAG_Tree).Length);
            Debug.Log("New tree has been spawned");
        }
    }

    private void SpawnTrees(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, trees.Length);
            Instantiate(trees[index], GenerateSpawnPositionXY(), Quaternion.identity, transform.parent);
        }
    }

    private Vector3 GenerateSpawnPositionXY()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnXRange, spawnXRange), Random.Range(-spawnYRange, spawnYRange), 0);
        return spawnPosition;
    }
}
