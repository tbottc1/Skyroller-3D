using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Transform player;

    public TMP_Text scoreText;
    public TMP_Text gameOverText;

    public float pointsPerSecond = 10f;
    public int platformBonus = 100;

    private float survivalTime = 0f;
    private int platformsCleared = 0;
    private int score = 0;

    private bool gameOver = false;

    void Start()
    {
        Time.timeScale = 1f;
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            return;
        }

        survivalTime += Time.deltaTime;

        score = Mathf.FloorToInt(survivalTime * pointsPerSecond) + platformsCleared * platformBonus;

        scoreText.text = "Score: " + score;
    }

    public void AddPlatformBonus()
    {
        platformsCleared++;
    }

    public void GameOver()
    {
        gameOver = true;

        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "You fell!\nFinal Score: " + score + "\nPress Space to Restart";

        Time.timeScale = 0f;
    }
}