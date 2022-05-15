using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameDirectionArrow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Rigidbody2D playerRb;
    private Transform playerTrans;
    private Rigidbody2D flameRb;
    private Rigidbody2D arrowRb;

    private readonly string TAG_Player = "Player";
    private readonly string TAG_Flame = "Flame";
    

    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag(TAG_Player).GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag(TAG_Player).GetComponent<Transform>();
        flameRb = GameObject.FindGameObjectWithTag(TAG_Flame).GetComponent<Rigidbody2D>();
        arrowRb = GetComponent<Rigidbody2D>();
        
    }


    private void Update()
    { 
            CalculateArrowRotation();
            CalculateArrowPosition();
    }

  private void CalculateArrowRotation()
    {
        Vector2 lookDir = flameRb.position - playerRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        arrowRb.rotation = angle;
    }

    private void CalculateArrowPosition()
    {
        arrowRb.transform.position = playerTrans.position + offset;
    }
}
