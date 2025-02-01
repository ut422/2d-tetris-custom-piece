using UnityEngine;  // import UnityEngine library

public class Tetromino : MonoBehaviour
{
    public float fallSpeed = 1.0f;  // tetromino fall speed
    private float fallTimer;  // timer to track fall time

    void Update()
    {
        if (GameStateManager.Instance.IsGameOver())
            return;

        fallTimer += Time.deltaTime;  // update fall timer
        if (fallTimer >= fallSpeed)  // if timer exceeds fall speed
        {
            MoveDown();  // move tetromino down
            fallTimer = 0;  // reset timer
        }

        // handle player input
        if (Input.GetKeyDown(KeyCode.LeftArrow))  // left arrow key
            Move(Vector3.left);  // move left

        if (Input.GetKeyDown(KeyCode.RightArrow))  // right arrow key
            Move(Vector3.right);  // move right

        if (Input.GetKeyDown(KeyCode.DownArrow))  // down arrow key
            MoveDown();  // move down immediately

        if (Input.GetKeyDown(KeyCode.UpArrow))  // up arrow key
            Rotate();  // rotate tetromino
    }

    // move tetromino in specified direction
    void Move(Vector3 direction)
    {
        transform.position += direction;  // move position
        if (!IsValidPosition())  // check validity
        {
            transform.position -= direction;  // revert if invalid
        }
    }

    // rotate tetromino clockwise
    void Rotate()
    {
        transform.Rotate(0, 0, 90);  // rotate 90 degrees
        if (!IsValidPosition())  // check validity
        {
            transform.Rotate(0, 0, -90);  // revert if invalid
        }
    }

    // move tetromino down one unit
    void MoveDown()
    {
        transform.position += Vector3.down;  // move down
        if (!IsValidPosition())  // check validity
        {
            transform.position -= Vector3.down;  // revert if invalid

            AddToGrid();  // add tetromino to grid
            GameGrid.DeleteFullRows();  // delete full rows

            // spawn new tetromino
            FindObjectOfType<TetrominoSpawner>().SpawnTetromino();

            enabled = false;  // stop updates for this tetromino
        }
    }

    // check if tetromino position is valid
    public bool IsValidPosition()
    {
        foreach (Transform child in transform)  // loop through all blocks
        {
            Vector2 pos = GameGrid.RoundPosition(child.position);  // round position

            if (!GameGrid.IsInsideGrid(pos))  // check if inside grid
                return false;  // return false if out of bounds

            if (GameGrid.grid[(int)pos.x, (int)pos.y] != null)  // check if occupied
                return false;  // return false if occupied
        }
        return true;  // return true if valid
    }

    // add tetromino blocks to grid
    void AddToGrid()
    {
        foreach (Transform child in transform)  // loop through blocks
        {
            Vector2 pos = GameGrid.RoundPosition(child.position);  // round position

            if (GameGrid.IsInsideGrid(pos))  // check if inside grid
                GameGrid.grid[(int)pos.x, (int)pos.y] = child;  // add to grid
        }
    }

    // draw gizmos for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // set gizmo color
        foreach (Transform child in transform)  // loop through blocks
        {
            Gizmos.DrawWireCube(child.position, Vector3.one);  // draw wireframe
        }
    }
}