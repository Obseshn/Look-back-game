using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlamePowerText : MonoBehaviour
{
    public Text flameText;
    private Flame flamePower;
    private float flameCounter;

    private string TAG_Flame = "Flame";

    private void Start()
    {
        flamePower = GameObject.FindGameObjectWithTag(TAG_Flame).GetComponent<Flame>();
    }
    private void Update()
    {
        flameCounter = flamePower._flamePower;
        flameText.text = flameCounter.ToString("0") + "%";
    }
}
