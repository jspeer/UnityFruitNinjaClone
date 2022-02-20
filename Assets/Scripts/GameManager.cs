using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score Elements")]
    public int score;
    public Text scoreText;
    public int highScore;
    public Text highScoreText;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public GameObject overlayItems;
    public Text gameOverPanelScoreText;
    public GameObject gameOverPanelHighScoreText;
    public GameObject gameOverPanelNewHighScoreText;
    [Header("Sounds")]
    public AudioClip[] sliceSounds;
    private AudioSource audioSource;

    private bool isNewHighScore = false;

    public void Awake()
    {
        // Advertisement.Initialize("myidhere");
        audioSource = GetComponent<AudioSource>();
        SetupGame();
    }

    private void SetupGame()
    {
        // Setup vars
        score = 0;
        scoreText.text = score.ToString();
        isNewHighScore = false;

        // Setup for panels
        gameOverPanel.SetActive(false);
        overlayItems.SetActive(true);

        // Setup for high scores
        GetHighScore();
        gameOverPanelHighScoreText.SetActive(true);
        gameOverPanelNewHighScoreText.SetActive(false);
        gameOverPanelHighScoreText.GetComponent<Text>().text = $"High Score: {highScore.ToString()}";
    }

    private void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
        highScoreText.text = $"High Score: {highScore.ToString()}";
    }

    public void IncreaseScore(int points)
    {
        // Increase the score
        score += points;
        scoreText.text = score.ToString();
        // Update high score
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("Highscore", highScore);
            highScoreText.text = $"High Score: {highScore.ToString()}";
            gameOverPanelHighScoreText.GetComponent<Text>().text = $"High Score: {highScore.ToString()}";
            isNewHighScore = true;
        }
    }

    public void OnBombHit()
    {
        // If you hit a bomb, pause the game
        Time.timeScale = 0;
        // Update the game over panel
        gameOverPanelScoreText.text = $"Score: {score.ToString()}";
        if (isNewHighScore) {
            gameOverPanelHighScoreText.SetActive(false);
            gameOverPanelNewHighScoreText.SetActive(true);
        }
        // Hide the game UI components
        overlayItems.SetActive(false);
        // Show the game over panel
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // Setup the game state
        SetupGame();
        // Destroy all game objects needed
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable")) {
            Destroy(g);
        }
        // Unpause the game
        Time.timeScale = 1;
    }

    public void PlayRandomSliceSound()
    {
        AudioClip randomSliceSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSliceSound);
    }

    public void QuitGame()
    {
        // die()?
        Application.Quit();
    }
}
