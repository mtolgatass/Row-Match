using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[] tiles;
    private BackgroundTile[,] gameBoard;
    public GameObject[,] allTiles;
    // Start is called before the first frame update
    void Start()
    {
        gameBoard = new BackgroundTile[width, height];
        allTiles = new GameObject[width, height];
        Configure();
    }

    private void Configure()
    {
        for (int i = 0; i < width; i ++)
        {
            for (int j = 0; j < height; j ++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity);
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "Background ( " + i + ", " + j + " )";

                int tileToUse = Random.Range(0, tiles.Length);
                GameObject tile = Instantiate(tiles[tileToUse], tempPosition, Quaternion.identity);
                tile.transform.parent = this.transform;
                tile.name = "Tile ( " + i + ", " + j + " )";
                allTiles[i, j] = tile;
            }
        }
    }
}
