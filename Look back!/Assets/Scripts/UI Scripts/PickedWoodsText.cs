using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickedWoodsText : MonoBehaviour
{
    public Text pickedWoodsText;


    private float countOfWoods;

    private PlayerController playerController;

    private readonly string TAG_Player = "Player";

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag(TAG_Player).GetComponent<PlayerController>();
    }

    private void Update()
    {
        countOfWoods = playerController.pickedWoods;
        pickedWoodsText.text = countOfWoods.ToString();
    }


}
