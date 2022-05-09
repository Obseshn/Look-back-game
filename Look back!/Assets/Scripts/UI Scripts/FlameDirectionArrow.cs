using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameDirectionArrow : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Transform playerTrans;
    private Rigidbody2D flameRb;
    private Rigidbody2D arrowRb;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        flameRb = GameObject.FindGameObjectWithTag("Flame").GetComponent<Rigidbody2D>();
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
