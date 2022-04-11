using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // MARK: - Private Variables
    private int column;
    private int row;
    private GameObject destinationTile;
    private int destinationTileRow;
    private Board board;
    private Vector2 firstTouchCoordinates;
    private Vector2 finalTouchCoordinates;
    private Vector2 tempPosition;
    private float swipeAngle = 0;
    private float swipeResist = .1f;

    // MARK: - Public Variables
    public Transform circleTransform;
    public Renderer sphereRenderer;
    public string color = "r";
    public bool canSwipe = true;

    void Start()
    {
        board = FindObjectOfType<Board>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;

    }

    void Update()
    {
        PerformChanges();
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
        if (Mathf.Abs(finalTouchCoordinates.y - firstTouchCoordinates.y) > swipeResist || Mathf.Abs(finalTouchCoordinates.x - firstTouchCoordinates.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchCoordinates.y - firstTouchCoordinates.y, finalTouchCoordinates.x - firstTouchCoordinates.x) * 180 / Mathf.PI;
            MoveTile();
        }
    }

    private void MoveTile()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column + 1 < board.width)
        {
            SwipeRightAction();
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row + 1 < board.height)
        {
            SwipeUpAction();
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            SwipeLeftAction();
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            SwipeDownAction();
        }
    }

    // MARK: - Private Swipe Functions
    private void SwipeRightAction()
    {
        destinationTile = board.allTiles[column + 1, row];

        bool canDestionationSwap = destinationTile.GetComponent<Tile>().canSwipe;
        if (canSwipe && canDestionationSwap)
        {
            destinationTile.GetComponent<Tile>().column -= 1;
            column += 1;
            board.moveCounter.DecreaseMoveCount();
        }
    }

    private void SwipeLeftAction()
    {
        destinationTile = board.allTiles[column - 1, row];

        bool canDestionationSwap = destinationTile.GetComponent<Tile>().canSwipe;
        if (canSwipe && canDestionationSwap)
        {
            destinationTile.GetComponent<Tile>().column += 1;
            column -= 1;
            board.moveCounter.DecreaseMoveCount();
        }
    }

    private void SwipeUpAction()
    {
        destinationTile = board.allTiles[column, row + 1];

        bool canDestionationSwap = destinationTile.GetComponent<Tile>().canSwipe;
        if (canSwipe && canDestionationSwap)
        {
            destinationTileRow = destinationTile.GetComponent<Tile>().row;
            destinationTileRow -= 1;
            destinationTile.GetComponent<Tile>().row -= 1;
            row += 1;
            board.moveCounter.DecreaseMoveCount();
        }
    }

    private void SwipeDownAction()
    {
        destinationTile = board.allTiles[column, row - 1];

        bool canDestionationSwap = destinationTile.GetComponent<Tile>().canSwipe;
        if (canSwipe && canDestionationSwap)
        {
            destinationTileRow = destinationTile.GetComponent<Tile>().row;
            destinationTileRow += 1;
            destinationTile.GetComponent<Tile>().row += 1;
            row -= 1;
            board.moveCounter.DecreaseMoveCount();
        }
    }

    // MARK: - Private Perform Change Functions

    private void CheckAndPerformHorizontalChanges()
    {
        if (Mathf.Abs(column - transform.position.x) > .1)
        {
            tempPosition = new Vector2(column, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .2f);

            int[] parameter = { row };
            board.FindMatches(parameter);
        }
        else
        {
            tempPosition = new Vector2(column, transform.position.y);
            transform.position = tempPosition;
            board.allTiles[column, row] = this.gameObject;

        }
    }

    private void CheckAndPerformVerticalChanges()
    {
        if (Mathf.Abs(row - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, row);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .3f);

            int[] parameter = { row, destinationTileRow };
            board.FindMatches(parameter);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, row);
            transform.position = tempPosition;
            board.allTiles[column, row] = this.gameObject;

        }
    }

    private void PerformChanges()
    {
        CheckAndPerformHorizontalChanges();
        CheckAndPerformVerticalChanges();
    }
}
