using UnityEngine;
using TMPro;  // for textmeshpro

public class TetrominoSpawner : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs;  // tetromino prefabs to spawn
    public TextMeshProUGUI gameOverText;  // game over text in UI

    // spawn a new tetromino
    public void SpawnTetromino()
    {
        // check if game is over, stop spawning if true
        if (GameGrid.IsGameOver())
        {
            ShowGameOver();  // show game over message
            return;
        }

        // select a random tetromino prefab
        int index = Random.Range(0, tetrominoPrefabs.Length);
        GameObject tetromino = Instantiate(tetrominoPrefabs[index], transform.position, Quaternion.identity);  // spawn tetromino

        // get tetromino script
        Tetromino tetrominoScript = tetromino.GetComponent<Tetromino>();

        // check if spawn position is valid
        if (!tetrominoScript.IsValidPosition())
        {
            Destroy(tetromino);  // destroy if invalid position
            ShowGameOver();  // show game over message
            return;
        }
    }

    private void Start()
    {
        SpawnTetromino();  // spawn first tetromino
    }

    // show game over message
    private void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);  // show game over text
        gameOverText.text = "you have'd lose!";  // set message
        GameStateManager.Instance.SetGameOver(true);
        enabled = false;  // disable spawner
    }
}