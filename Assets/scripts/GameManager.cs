using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public float gameTime = 60f;

    private float remainingTime;
    private int score;
    private bool gameIsOver = false;

    private void Start()
    {
        remainingTime = gameTime;
        score = 0;
        StartCoroutine(UpdateTimer());
        UpdateScoreText();
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = Mathf.FloorToInt(remainingTime).ToString();
            yield return null;
        }

        EndGame();
    }

    private void Update()
    {
        if (gameIsOver)
        {
            SceneManager.LoadScene("GameOver");

        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void AddTime(float amount)
    {
        Debug.Log("oi");
        remainingTime += amount;
    }

    public void RemoveTime(float amount)
    {
        remainingTime -= amount;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void EndGame()
    {
        gameIsOver = true;
        
    }
}
