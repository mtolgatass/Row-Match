using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int initialColumn;
    public int initialRow;
    public int destinationColumn;
    public int destinationRow;
    private GameObject destinationTile;
    private Board board;
    private Vector2 firstTouchCoordinates;
    private Vector2 finalTouchCoordinates;
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        destinationColumn = (int)transform.position.x;
        destinationRow = (int)transform.position.y;

        initialRow = destinationRow;
        initialColumn = destinationColumn;
    }

    // Update is called once per frame
    void Update()
    {
        destinationColumn = initialColumn;
        destinationRow = initialRow;
        if(Mathf.Abs(destinationColumn - transform.position.x) > .1)
        {
            // Move Towards
            tempPosition = new Vector2(destinationColumn, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        } else
        {
            tempPosition = new Vector2(destinationColumn, transform.position.y);
            transform.position = tempPosition;
            board.allTiles[initialColumn, initialRow] = this.gameObject;
        }

        if (Mathf.Abs(destinationRow - transform.position.y) > .1)
        {
            // Move Towards
            tempPosition = new Vector2(transform.position.x, destinationRow);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, destinationRow);
            transform.position = tempPosition;
            board.allTiles[initialColumn, initialRow] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finalTouchCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchCoordinates.y - firstTouchCoordinates.y, finalTouchCoordinates.x - firstTouchCoordinates.x) * 180 / Mathf.PI;
        MoveTile();
    }

    void MoveTile()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && initialColumn < board.width)
        {
            // Swipe Right
            destinationTile = board.allTiles[initialColumn + 1, initialRow];
            destinationTile.GetComponent<Tile>().initialColumn -= 1;
            initialColumn += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && initialRow < board.height)
        {
            // Swipe Up
            destinationTile = board.allTiles[initialColumn, initialRow + 1];
            destinationTile.GetComponent<Tile>().initialRow -= 1;
            initialRow += 1;
        } else if ((swipeAngle > 135 || swipeAngle <= -135) && initialColumn > 0)
        {
            // Swipe Left
            destinationTile = board.allTiles[initialColumn - 1, initialRow];
            destinationTile.GetComponent<Tile>().initialColumn += 1;
            initialColumn -= 1;
        } else if (swipeAngle < -45 && swipeAngle >= -135 && initialRow > 0)
        {
            // Swipe Dow
            destinationTile = board.allTiles[initialColumn, initialRow - 1];
            destinationTile.GetComponent<Tile>().initialRow += 1;
            initialRow -= 1;
        }
    }
}
