using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsController : MonoBehaviour
{
    
    [SerializeField] private float liveTimer;
    [SerializeField] Vector2 randomDir;


    private Rigidbody2D itemRb;

    private readonly float liveTime = 12f;


    private void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        liveTimer = liveTime;
       // itemRb.AddForce(GetRandomDirection() / soarMoveSpeed, ForceMode2D.Impulse);

    }
    private void Update()
    {
        itemRb.velocity = Vector2.zero;
        liveTimer -= Time.deltaTime;
        if (liveTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
