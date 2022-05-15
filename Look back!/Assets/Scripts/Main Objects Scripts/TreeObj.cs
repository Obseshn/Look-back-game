using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObj : MonoBehaviour
{
    public GameObject player;
    public GameObject woodParticle;

    [SerializeField] private float health;
    [SerializeField] private GameObject woodDrop;

    private readonly float minimumWoodDrop = 6;


    private void Start()
    {
        health = Random.Range(20, 30);
    }

    public void TakeDamage(float damage)
    {
        if (health <= damage)
        {
            DropInstantiate();
            Die();
        }
        Instantiate(woodParticle, transform.position, transform.rotation);
        health -= damage;
        Debug.Log("Tree taked: " + damage + " dmg!");
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void DropInstantiate()
    {
        float newDrop = Random.Range(minimumWoodDrop, minimumWoodDrop * 2);
        Vector3 position = transform.position;
        for (int i = 0; i < newDrop; i++)
        {
            Instantiate(woodDrop, position, Quaternion.identity);
        }
        Debug.Log(newDrop + " wood inst");
    }
}
