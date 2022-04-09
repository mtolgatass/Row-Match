using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // MARK: - Private Variables
    private int column;
    private int row;
    private GameObject destinationTile;
    private Board board;
    private Vector2 firstTouchCoordinates;
    private Vector2 finalTouchCoordinates;
    private Vector2 tempPosition;
    private float swipeAngle = 0;

    void Start()
    {
        board = FindObjectOfType<Board>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;

    }

    void Update()
    {
        performChanges();
    }

    // MARK: - Private Functions

    private void OnMouseDown()
    {
        firstTouchCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finalTouchCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchCoordinates.y - firstTouchCoordinates.y, finalTouchCoordinates.x - firstTouchCoordinates.x) * 180 / Mathf.PI;
        MoveTile();
    }

    private void MoveTile()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column + 1 < board.width)
        {
            // Swipe Right
            SwipeRightAction();
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row + 1 < board.height)
        {
            // Swipe Up
            SwipeUpAction();
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            // Swipe Left
            SwipeLeftAction();
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Swipe Down
            SwipeDownAction();
        }
    }

    // MARK: - Private Swipe Functions
    private void SwipeRightAction()
    {
        destinationTile = board.allTiles[column + 1, row];
        destinationTile.GetComponent<Tile>().column -= 1;
        column += 1;
    }

    private void SwipeLeftAction()
    {
        destinationTile = board.allTiles[column - 1, row];
        destinationTile.GetComponent<Tile>().column += 1;
        column -= 1;
    }

    private void SwipeUpAction()
    {
        destinationTile = board.allTiles[column, row + 1];
        destinationTile.GetComponent<Tile>().row -= 1;
        row += 1;
    }

    private void SwipeDownAction()
    {
        destinationTile = board.allTiles[column, row - 1];
        destinationTile.GetComponent<Tile>().row += 1;
        row -= 1;
    }

    // MARK: - Private Perform Change Functions

    private void checkAndPerformHorizontalChanges()
    {
        if (Mathf.Abs(column - transform.position.x) > .1)
        {
            tempPosition = new Vector2(column, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .3f);
        }
        else
        {
            if (row != transform.position.y)
            {
                tempPosition = new Vector2(column, transform.position.y);
                transform.position = tempPosition;
                board.allTiles[column, row] = this.gameObject;
            }
        }
    }

    private void checkAndPerformVerticalChanges()
    {
        if (Mathf.Abs(row - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, row);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .3f);
        }
        else
        {
            if (column != transform.position.x)
            {
                tempPosition = new Vector2(transform.position.x, row);
                transform.position = tempPosition;
                board.allTiles[column, row] = this.gameObject;
            }
        }
    }

    private void performChanges()
    {
        checkAndPerformHorizontalChanges();
        checkAndPerformVerticalChanges();
    }
}
