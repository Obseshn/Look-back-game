using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private float flamePower;
    public float _flamePower
    {
        get { return flamePower; }
    }
    private Animator flameAnimator;
    private string TAG_flamePower = "Flame Power";

    private void Start()
    {
        flamePower = 100f;
        flameAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        flamePower -= Time.deltaTime;
        flameAnimator.SetFloat(TAG_flamePower, flamePower);
    }


    public void IncreaseFlame(float addingFlamePower)
    {
        flamePower += addingFlamePower;
        Debug.Log("Flame was increased on: " + flamePower);
    }


}
