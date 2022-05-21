using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private float flamePower;

    private float levelDificultyCoeff;
    
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
        levelDificultyCoeff = CalculateDifficulty();
    }

    private void Update()
    {
        if (flamePower >= 0)
        {
            flamePower -= Time.deltaTime * levelDificultyCoeff;
            flameAnimator.SetFloat(TAG_flamePower, flamePower);
            particleSpeed = flamePower / 50f;
            fireParticle.startLifetime = particleSpeed;
            fireParticle.startSpeed = particleSpeed;
        }
        else
        {
            FindObjectOfType<GameManager>().GameEnd();
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

    private float CalculateDifficulty()
    {
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 1:
                return 1f;
            case 2:
                return 1.2f;
            case 3:
                return 1.4f;
            default:
                Debug.LogError("Level difficulty isn't defined,try to check MainMenuController");
                return 0;
        }
    }
}
