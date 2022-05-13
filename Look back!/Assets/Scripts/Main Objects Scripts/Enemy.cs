using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isOnStop = false;

    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private float activeSpeed;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float distanceToJump = 5f;
    [SerializeField] private float dashCounter;
    [SerializeField] private float liveTime;


    private float enemyFOVDistance = 10f;
    private float moveSpeed = 2f;
    private float dashSpeed = 5f;
    private float dashCoolDown = 5f;
    private float minLiveTime = 20f;
    

    private Vector2 dashTarget;
   
    private Rigidbody2D targetRb;
    private Rigidbody2D enemyRb;

    private SpriteRenderer enemySprtRender;

    private Transform playerTrans;

    private bool dashActive = false;
    private bool isVisible = false;
    


    // Animator conditions
    private string TAG_Attack = "Attack";
    private string NAME_SOUND_EnemyRoar = "Enemy_Roar";




    private void Start()
    {
        targetRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        enemyRb = GetComponent<Rigidbody2D>();
        activeSpeed = moveSpeed;
        liveTime = Random.Range(minLiveTime, minLiveTime * 3);
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemySprtRender = GetComponent<SpriteRenderer>();
        enemySprtRender.enabled = false;
    }

    private void Update()
    {
        

        if (!isOnStop)
        {
            if (Vector2.Distance(enemyRb.position, targetRb.position) < enemyFOVDistance) // Enable enemy behaviour when player into it's FOV
            {

                if (dashCounter > -1)
                {
                    dashCounter -= Time.deltaTime;
                }

                if (Vector2.Distance(targetRb.position, enemyRb.position) < distanceToJump && !dashActive && dashCounter <= 0) // Conditions for enemy dash
                {
                    Dash();
                }
                else if (!dashActive) //  conditions for normal enemy movement
                {
                    RotateEnemyToTarget(); // Rotate enemy to player  
                    transform.position = Vector2.MoveTowards(transform.position, targetRb.transform.position, activeSpeed * Time.deltaTime); // Move enemy to player past position
                    enemyAnimator.SetBool("isWalking", true);
                }
                else if (dashActive) // conditions for dash enemy movement
                {
                    transform.position = Vector2.MoveTowards(transform.position, dashTarget, activeSpeed * Time.deltaTime); // Move enemy to player current position
                    enemyAnimator.SetBool("isWalking", false);
                }

            }
            if (Vector2.Distance(enemyRb.position, targetRb.position) > enemyFOVDistance * 1.5f) // Reset dash C/D when player so far
            {
                
                dashCounter = dashCoolDown;
                liveTime -= Time.deltaTime;
                if (liveTime <= 0)
                {
                    Destroy(gameObject);
                }
                enemyAnimator.SetBool("isWalking", false);
            }
        }
    }

    private void RotateEnemyToTarget()
    {
        Vector2 lookDir = targetRb.GetComponent<Rigidbody2D>().position - enemyRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        enemyRb.rotation = angle;
    }

    void Dash()
    {
        dashActive = true;
       // enemyAnimator.SetBool(TAG_Attack, true);

        dashTarget = targetRb.transform.position;

        StartCoroutine(DashAwake(1));
        FindObjectOfType<AudioManager>().PlaySound(NAME_SOUND_EnemyRoar + GetRandomRoarIndex()); // Play sound 
    }

    private string GetRandomRoarIndex()
    {
        int randomIndex = Random.Range(1, 5);
        Debug.Log(randomIndex);
        return randomIndex.ToString();
    }

    IEnumerator DashAwake(float seconds)
    {
    
        dashCounter = dashCoolDown + 3f;
        activeSpeed = 0;
        

        yield return new WaitForSeconds(seconds);
        enemyAnimator.SetBool(TAG_Attack, true);
        activeSpeed = dashSpeed;
        FindObjectOfType<AudioManager>().PlaySound("Enemy_Jump");
        
        yield return new WaitForSeconds(seconds);
        activeSpeed = moveSpeed;
        dashActive = false;
        enemyAnimator.SetBool(TAG_Attack, false);
    }

    public void StopEnemy(float timeToStop)
    {
        StartCoroutine(StopEnemyAwake(timeToStop));
    }

    IEnumerator StopEnemyAwake(float seconds)
    {
        Debug.Log("Stop start");
        FindObjectOfType<AudioManager>().PlaySound("Enemy_OnStop");
        isOnStop = true;
        yield return new WaitForSeconds(seconds);
        isOnStop = false;
        Debug.Log("Stop stop");
    }

    IEnumerator EnemyDisappear()
    {
        isVisible = false;
        yield return new WaitForSeconds(2.5f);
        if (!isVisible)
        {
            enemySprtRender.enabled = false;
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse Lighter") && isVisible == false)
        {
            isVisible = true;
            enemySprtRender.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse Lighter") && isVisible == true)
        {
            StartCoroutine(EnemyDisappear());
        }
    }
}
