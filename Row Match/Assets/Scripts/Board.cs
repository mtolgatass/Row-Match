using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    private BackgroundTile[,] gameBoard;
    // Start is called before the first frame update
    void Start()
    {
        gameBoard = new BackgroundTile[width, height];
        Configure();
    }

    private void Configure()
    {
        for (int i = 0; i < width; i ++)
        {
            for (int j = 0; j < height; j ++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";
            }
        }
    }
}
