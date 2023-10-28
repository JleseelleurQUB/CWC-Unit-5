using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private GameObject titleScreen;
    
    public bool isGameActive;
    private float spawnRate = 1.0f;
    private int score;
    private int scoreMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("Title Screen");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        scoreMultiplier = difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd * scoreMultiplier;
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
