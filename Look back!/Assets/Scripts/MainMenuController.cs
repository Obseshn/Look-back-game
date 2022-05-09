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
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");

    }
}
