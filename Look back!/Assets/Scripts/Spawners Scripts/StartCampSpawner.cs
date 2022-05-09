using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCampSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject flame;
    private float border = 30f;
    private float playerOffset = 4f;
    

    private void Awake()
    {
        SpawnCamp(player, flame);
    }
    
    private void SpawnCamp(GameObject player, GameObject flame)
    {
        Vector3 randomSpawnPosition = GenerateRandomSpawnPosition2D();
        Instantiate(flame, randomSpawnPosition, transform.rotation);
        Vector3 randomOffset = GeneratePlayerFlameOffset();
        Instantiate(player, randomSpawnPosition + randomOffset, transform.rotation);
    }

    private Vector3 GenerateRandomSpawnPosition2D()
    {
        float randX = Random.Range(-border, border);
        float randY = Random.Range(-border, border);

        return new Vector3(randX, randY, 0);
    }

    private Vector3 GeneratePlayerFlameOffset()
    {
        float offsetX = Random.Range(playerOffset, playerOffset * 2);
        float offsetY = Random.Range(playerOffset, playerOffset * 2);

        return new Vector3(offsetX, offsetY, 0);
    }
}
