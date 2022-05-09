﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    public int pickedWoods;
    private GameObject arrow;
    public int _pickedWoods
    {
        get { return pickedWoods; }
    }


    private PlayerFOV fieldOfView;
    [SerializeField] private Animator playerAnimator;
    private Animator mainCamAnim;
    private float moveSpeed = 4f;


    [SerializeField] private Transform attackCirclePos;
    [SerializeField] private float attackCircleRadius;
    [SerializeField] private LayerMask Trees;
    private float attackCD = 1f;
    private float timeBtwAttack;
    private float flameRadius = 7f;
    private float stealCD = 10f;
    private float stealCDTimer;


    // Player borders
    private float moveBorder = 70f;


    private int damage = 5;

    private Rigidbody2D playerRb;
    private GameObject dropWoodText;

    private Rigidbody2D flameRb;
    private Flame flame;

    private Vector2 movement;
    private Vector2 mousePos;

    private Color treeColor;

    private string WALK_COND_NAME = "isWalking";
    private string ATTACK_COND_NAME = "Attack";

    



    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        flameRb = GameObject.FindGameObjectWithTag("Flame").GetComponent<Rigidbody2D>();
        flame = GameObject.FindGameObjectWithTag("Flame").GetComponent<Flame>();
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCamAnim = cam.GetComponent<Animator>();
        fieldOfView = GameObject.FindGameObjectWithTag("Player FOV").GetComponent<PlayerFOV>();
        dropWoodText = GameObject.FindGameObjectWithTag("Drop Woods Text");
    }

    private void Start()
    {
        pickedWoods = 0;
    }
    private void Update()
    {
        if (stealCDTimer >= 0)
        {
            stealCDTimer -= Time.deltaTime;
        }
        // Movement code start
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = (playerRb.position - mousePos);

        if (movement != Vector2.zero)
        {
            playerAnimator.SetBool(WALK_COND_NAME, true);
        }
        else
        {
            playerAnimator.SetBool(WALK_COND_NAME, false);
        }
        // Movement code end

        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);


        // Attack code start
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Attack();
                timeBtwAttack = attackCD;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        // Attack code end

        // Drop wood and arrow active conditions start
        if (Vector2.Distance(playerRb.position, flameRb.position) < flameRadius)
        {
            if (Input.GetKeyDown(KeyCode.Q) && pickedWoods > 0)
            {
                DropWoods(pickedWoods);
            }
            arrow.SetActive(false);
            dropWoodText.SetActive(true);
        }
        else if(Vector2.Distance(playerRb.position, flameRb.position) > flameRadius) {
            arrow.SetActive(true);
            dropWoodText.SetActive(false);
        }
        // Drop wood and arrow active conditions end



    }

    private void FixedUpdate()
    {
        if (transform.position.x >= moveBorder)
        {
            Vector3 tempXPos = new Vector3(moveBorder, transform.position.y, transform.position.z);
            transform.position = tempXPos;
            
        }
        if (transform.position.x <= -moveBorder)
        {
            Vector3 tempXPos = new Vector3(-moveBorder, transform.position.y, transform.position.z);
            transform.position = tempXPos;
            
        }
        if (transform.position.y >= moveBorder)
        {
            Vector3 tempYPos = new Vector3(transform.position.x, moveBorder, transform.position.z);
            transform.position = tempYPos;
            
        }
        if (transform.position.y <= -moveBorder)
        {
            Vector3 tempYPos = new Vector3(transform.position.x, -moveBorder, transform.position.z);
            transform.position = tempYPos;
        }

        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
        CalculatePlayerRotation(); // Rotate player
    }

    private void CalculatePlayerRotation()
    {
        Vector2 lookDir = mousePos - playerRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRb.rotation = angle;
    }

    private void DropWoods(int woods)
    {
        flame.IncreaseFlame(woods);
        pickedWoods = 0;
        Debug.Log("Woods have been droped");
    }

    private void Attack()
    {
        Collider2D[] treesToDamage = Physics2D.OverlapCircleAll(attackCirclePos.position, attackCircleRadius, Trees);
        for (int i = 0; i < treesToDamage.Length; i++)
        {
            treesToDamage[i].GetComponent<Tree>().TakeDamage(damage);
        }
        
        StartCoroutine(AttackDuration());
    }

    IEnumerator AttackDuration()
    {
        playerAnimator.SetBool(ATTACK_COND_NAME, true);
        mainCamAnim.Play("MainCamera_Shake");
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool(ATTACK_COND_NAME, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            treeColor = collision.gameObject.GetComponent<SpriteRenderer>().material.color;
            treeColor.a = 0.6f;
            collision.gameObject.GetComponent<SpriteRenderer>().material.color = treeColor;
        }

        if (pickedWoods > 0 && stealCDTimer <= 0)
        {
            if (collision.CompareTag("Enemy"))
            {
                pickedWoods -= Random.Range(0, pickedWoods / 2);
                Debug.Log("Enemy steal your woods!");
                stealCDTimer = stealCD;
            }
        }
        


       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            treeColor.a = 1f;
            collision.gameObject.GetComponent<SpriteRenderer>().material.color = treeColor;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackCirclePos.position, attackCircleRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Drop_wood"))
        {
            pickedWoods++;
            Destroy(collision.gameObject);
        }
    }
}
