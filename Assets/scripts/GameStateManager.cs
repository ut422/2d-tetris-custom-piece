using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // singleton instance of GameStateManager
    public static GameStateManager Instance { get; private set; }
    private bool isGameOver = false;

    // called when the script is loaded
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Set game over state and handle related actions
    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;
        if (isGameOver)
        {
            DisableAllTetrominoMovement();
            Time.timeScale = 0;
        }
    }

    // Check if game is over
    public bool IsGameOver()
    {
        return isGameOver;
    }

    // disable all active Tetromino movements
    private void DisableAllTetrominoMovement()
    {
        Tetromino[] activeTetrominos = FindObjectsOfType<Tetromino>();
        foreach (Tetromino t in activeTetrominos)
        {
            t.enabled = false;
        }
    }
}
