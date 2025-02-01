using TMPro;  // import TextMeshPro for text UI
using UnityEngine;  // import UnityEngine for game functionality

public class GameTimer : MonoBehaviour
{
    // singleton instance of GameTimer
    public static GameTimer Instance;

    // references for timer and game over text UI
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;

    // time limit and score target
    public float timeLimit = 60f;
    public int scoreTarget = 500;

    // remaining time and game over flag
    private float timeRemaining;
    private bool gameEnded = false;

    // called when the script is loaded
    void Awake()
    {
        // set the singleton instance
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);  // destroy extra instances
    }

    // called at the start
    void Start()
    {
        timeRemaining = timeLimit;  // set initial time
        UpdateTimerText();  // update timer display
        gameOverText.gameObject.SetActive(false);  // hide game over text
    }

    // called every frame
    void Update()
    {
        if (gameEnded || GameStateManager.Instance.IsGameOver()) return;  // if game is over, do nothing

        timeRemaining -= Time.deltaTime;  // decrease time
        UpdateTimerText();  // update timer display

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;  // make sure time doesn't go below 0
            CheckGameEnd(false);  // check if the game is over (lost)
        }
    }

    // updates the timer text
    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();  // show rounded time
    }

    // checks if the game has ended (win or lose)
    public void CheckGameEnd(bool won)
    {
        if (gameEnded || GameStateManager.Instance.IsGameOver()) return;  // if game is already over, do nothing

        gameEnded = true;  // mark game as ended
        gameOverText.text = won ? "You Win!" : "Game Over!";  // set game over text based on win/lose
        gameOverText.gameObject.SetActive(true);  // show game over text
        GameStateManager.Instance.SetGameOver(true);
    }
}