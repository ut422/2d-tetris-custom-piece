using UnityEngine;

public class GameGrid : MonoBehaviour
{
    // grid dimensions
    public static int width = 10;
    public static int height = 20;
    // 2D array to store grid blocks
    public static Transform[,] grid = new Transform[width, height];

    // reference to ScoreManager
    private ScoreManager scoreManager;

    // initialize scoreManager reference
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // round position to nearest grid cell
    public static Vector2 RoundPosition(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    // check if position is inside the grid
    public static bool IsInsideGrid(Vector2 pos)
    {
        return (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height);
    }

    // delete a full row
    public static void DeleteRow(int row)
    {
        // destroy blocks in row
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, row]?.gameObject);
            grid[x, row] = null;
        }

        // move blocks above the row down
        for (int y = row + 1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];  // move block down
                    grid[x, y - 1].position += Vector3.down;  // update position
                    grid[x, y] = null;  // clear original cell
                }
            }
        }
    }

    // check if a row is full
    public static bool IsRowFull(int row)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, row] == null)  // check for empty cell
                return false;  // row is not full
        }
        return true;  // row is full
    }

    // delete all full rows and update score
    public static void DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))  // check if row is full
            {
                DeleteRow(y);  // delete the row
                // add points for cleared line
                FindObjectOfType<ScoreManager>().IncreaseScore(100);  // 100 points per row
                y--;  // check same row again after shift
            }
        }
    }

    // check if the game is over (blocks in top row)
    public static bool IsGameOver()
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, height - 1] != null)  // block in top row
                return true;  // game over
        }
        return false;  // game not over
    }
}