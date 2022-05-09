using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickedWoodsText : MonoBehaviour
{
    private float countOfWoods;
    public Text pickedWoodsText;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        countOfWoods = playerController._pickedWoods;
        pickedWoodsText.text = countOfWoods.ToString();
    }


}
