using TMPro;  // for textmeshpro functionality
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // score display text
    public TextMeshProUGUI winText;   // "you win!" message text
    private int score = 0;  // initial score
    public int scoreTarget = 1000;  // score target to win

    // initialize score at the start
    void Start()
    {
        UpdateScoreText();  // display initial score
        winText.gameObject.SetActive(false);  // hide win message initially
    }

    // increase score when a line is cleared
    public void IncreaseScore(int points)
    {
        score += points;  // add points
        UpdateScoreText();  // update score display

        if (score >= scoreTarget)  // check if target is reached
        {
            DisplayWinMessage();  // show win message
        }
    }

    // update score text in UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();  // show score
    }

    // show win message when score target is reached
    private void DisplayWinMessage()
    {
        winText.gameObject.SetActive(true);  // show win message
        winText.text = "Your winner!";  // set win text
        GameStateManager.Instance.SetGameOver(true);
    }
}