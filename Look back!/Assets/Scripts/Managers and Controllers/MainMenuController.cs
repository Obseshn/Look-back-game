using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text HighScoreText;

    private float highScore;

    private void Start()
    {
        highScore = Mathf.RoundToInt(PlayerPrefs.GetFloat("HighScore", 0f));
        HighScoreText.text = "Best time: " + highScore.ToString();
        PlayerPrefs.SetInt("Difficulty", 0);
    }
    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("Difficulty") != 0)
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1f;
            }
            SceneManager.LoadScene("GamePlay");
        }
        Debug.Log("Player don't change a difficulty level!");
    }

    public void SetDifficultyLevel(int level)
    {

        if (level == 1)
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        else if (level == 2)
        {
            PlayerPrefs.SetInt("Difficulty", 2);
        }
        else if (level == 3)
        {
            PlayerPrefs.SetInt("Difficulty", 3);
        }
    }
}
