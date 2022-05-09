﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsController : MonoBehaviour
{
    private float liveTime = 12f;
    [SerializeField] private float liveTimer;
    [SerializeField] private float soarMoveSpeed = 10f;
    private Rigidbody2D itemRb;
    [SerializeField] Vector2 randomDir;

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

    private Vector2 GetRandomDirection()
    {
        randomDir = new Vector2();
        randomDir.x = Random.Range(0, 2);
        randomDir.y = Random.Range(0, 2);
        return randomDir;
    }
}
