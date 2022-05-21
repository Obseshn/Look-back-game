using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;

    public Text timerText;
    public GameObject GameOverMenu;

    private float time = 0;
    private float flamePower;



    private void Start()
    {
        time = 0;
    }


    

    private void Update()
    {
        time += Time.deltaTime;
        timerText.text = time.ToString("0");


        flamePower = GameObject.FindGameObjectWithTag("Flame").GetComponent<Flame>()._flamePower;
        if (flamePower <= 0 && !gameOver)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        if (time > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", time);
            Debug.Log("HighScore has been update!");
            GameOverMenu.SetActive(true);
            Time.timeScale = 0f;
            gameOver = true;
        }
    }
}
