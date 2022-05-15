using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public bool lightShoot = false;

    [SerializeField] private Transform lightStopCirclePos;
    [SerializeField] private float lightStopCirlceRad;
    [SerializeField] private LayerMask enemies;

    private Light pointLight;

    private readonly float lightPowerTime = 0.2f;
    private readonly float lightPowerCD = 2f;
    private float lightPowerCDtimer;

    

    private void Start()
    {
        pointLight = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<Light>();
    }

    
    private void Update()
    {
        if (lightPowerCDtimer >= 0)
        {
            lightPowerCDtimer -= Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.Mouse0) && lightPowerCDtimer <= 0)
        {
            IncreaseLightPower();
        }
    }
    public void IncreaseLightPower()
    {
        FindObjectOfType<AudioManager>().PlaySound("FlashLight_On");
        lightShoot = true;
        StartCoroutine(LightUp(50));
        lightPowerCDtimer = lightPowerCD;
        Collider2D[] enemiesToStop = Physics2D.OverlapCircleAll(lightStopCirclePos.position, lightStopCirlceRad, enemies);
        for (int i = 0; i < enemiesToStop.Length; i++)
        {
            enemiesToStop[i].GetComponent<Enemy>().StopEnemy(3f);
        }
    }

    IEnumerator LightUp(float addingLight)
    {
        pointLight.intensity += addingLight;
        yield return new WaitForSeconds(lightPowerTime);
        pointLight.intensity -= addingLight;
        lightShoot = false;
        Debug.Log("FlashLight has been power Up");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lightStopCirclePos.position, lightStopCirlceRad);
    }
}
