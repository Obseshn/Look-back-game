using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject player;
    public GameObject woodParticle;

    [SerializeField] private float health;
    [SerializeField] private GameObject woodDrop;

    private int minimumWoodDrop = 6;


    private void Start()
    {
        health = Random.Range(20, 30);
    }

    private void Update()
    {
        
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
        int newDrop = Random.Range(minimumWoodDrop, minimumWoodDrop * 2);
        player.GetComponent<PlayerController>().pickedWoods += newDrop;
        Vector3 position = transform.position;
        for (int i = 0; i < newDrop; i++)
        {
            Instantiate(woodDrop, position, Quaternion.identity);
        }
        Debug.Log(newDrop + " wood inst");
    }
}
