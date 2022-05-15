using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private float flamePower;
    public float _flamePower
    {
        get {
            return flamePower;
            }
    } 

    private Animator flameAnimator;
    private ParticleSystem fireParticle;
    private float particleSpeed;

    private readonly string TAG_flamePower = "Flame Power";

    private void Start()
    {
        flamePower = 100f;
        flameAnimator = GetComponent<Animator>();
        fireParticle = GameObject.FindGameObjectWithTag("Particle_Fire").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        
        flamePower -= Time.deltaTime;
        flameAnimator.SetFloat(TAG_flamePower, flamePower);
        if (flamePower > 0)
        {
            particleSpeed = flamePower / 50f;
            fireParticle.startLifetime = particleSpeed;
            fireParticle.startSpeed = particleSpeed;

        }
        
    }


    public void IncreaseFlame(float addingFlamePower)
    {
        flamePower += addingFlamePower;
        Debug.Log("Flame was increased on: " + flamePower);
    }

    public float GetFlamePower()
    {
        return flamePower;
    }
}
