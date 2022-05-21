using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    // Getted objects and variables: 
    public GameObject ParticleHolder;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform attackCirclePos;
    [SerializeField] private float attackCircleRadius;
    [SerializeField] private LayerMask Trees;
    private Camera cam;
    private GameObject arrow;
    private PlayerFOV fieldOfView;
    private Animator mainCamAnim;
    private GameObject dropWoodText;
    private Rigidbody2D flameRb;
    private Flame flame;




    // Player variables 
    public int pickedWoods;

    private readonly float moveSpeed = 4f;
    private readonly float attackCD = 1f;
    private float timeBtwAttack;
    private readonly float flameRadius = 7f;
    private readonly float stealCD = 10f;
    private float stealCDTimer;
    private readonly float damage = 5;
    private readonly float moveBorder = 70f;
    private Vector2 movement;
    private Vector2 mousePos;

    private AudioSource playerAudio;
    private Rigidbody2D playerRb;

    // TAGS and other
    private readonly string WALK_COND_NAME = "isWalking";
    private readonly string ATTACK_COND_NAME = "Attack";
    private Color treeColor;
    private readonly int FlameSoundNumber = 4;


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
        playerAudio = gameObject.GetComponent<AudioSource>();
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
            ParticleHolder.SetActive(true);
            playerAudio.mute = false;


        }
        else
        {
            playerAnimator.SetBool(WALK_COND_NAME, false);
            ParticleHolder.SetActive(false);
            playerAudio.mute = true;
        }
        // Movement code end

        // FOV(not mouse ligter) code
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
            FindObjectOfType<AudioManager>().sounds[FlameSoundNumber].volume = 0f;

            if (Input.GetKeyDown(KeyCode.Q) && pickedWoods > 0)
            {
                DropWoods(pickedWoods);
            }

            arrow.SetActive(false);
            dropWoodText.SetActive(true);
        }
        else if (Vector2.Distance(playerRb.position, flameRb.position) > flameRadius) 
        {
            FindObjectOfType<AudioManager>().sounds[FlameSoundNumber].volume = 0.3f;
            FindObjectOfType<AudioManager>().PlaySound("Flame_Sound");

            arrow.SetActive(true);
            dropWoodText.SetActive(false);
        }
        // Drop wood and arrow active conditions end



    }

    private void FixedUpdate()
    {
        // Border checker start
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
        // Border checker end


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
        FindObjectOfType<AudioManager>().PlaySound("Drop_Woods");
        flame.IncreaseFlame(woods);
        pickedWoods = 0;
        Debug.Log("Woods have been droped");
    }

    private void Attack()
    {
        Collider2D[] treesToDamage = Physics2D.OverlapCircleAll(attackCirclePos.position, attackCircleRadius, Trees);
        if (treesToDamage.Length == 0)
        {
            FindObjectOfType<AudioManager>().PlaySound("Axe_Missing");
            Debug.Log("Axe Miss");
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySound("Axe_Attack");
            Debug.Log("Axe Attack");
        }
        for (int i = 0; i < treesToDamage.Length; i++)
        {
            treesToDamage[i].GetComponent<TreeObj>().TakeDamage(damage);
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
        } // Decrease tree opacity

        if (pickedWoods > 0 && stealCDTimer <= 0)
        {
            if (collision.CompareTag("Enemy"))
            {
                pickedWoods -= collision.gameObject.GetComponent<Enemy>().StealWoods();
                Debug.Log("Enemy steal your woods!");
                stealCDTimer = stealCD;
            }
        } // Let enemy steal woods
        


       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            treeColor.a = 1f;
            collision.gameObject.GetComponent<SpriteRenderer>().material.color = treeColor; // Increase tree opacity in Exit
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackCirclePos.position, attackCircleRadius); // Draws attack circle
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Drop_wood"))
        {
            FindObjectOfType<AudioManager>().PlaySound("Take_Wood");
            pickedWoods++;
            Destroy(collision.gameObject);
        }
    }
}
