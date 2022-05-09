using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private float timeCounter;
    private void Update()
    {
        timeCounter += Time.deltaTime;
        timerText.text = timeCounter.ToString("0");
    }
}
