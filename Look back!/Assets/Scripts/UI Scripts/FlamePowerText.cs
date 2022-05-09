using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlamePowerText : MonoBehaviour
{
    public Text flameText;
    private Flame flamePower;
    private float flameCounter;

    private void Start()
    {
        flamePower = GameObject.FindGameObjectWithTag("Flame").GetComponent<Flame>();
    }
    private void Update()
    {
        flameCounter = flamePower._flamePower;
        flameText.text = flameCounter.ToString("0") + "%";
    }
}
