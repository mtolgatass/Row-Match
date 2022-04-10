using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Board : MonoBehaviour
{
    // MARK: - Private Variables
    private BackgroundTile[,] gameBoard;
    private bool matchFound = false;
    private List<int> matchedRows = new List<int>(); //TODO: MATCHED ROWS ARE HERE
    private List<string> matchedColors = new List<string>();
    private List<string> grid = new List<string>();

    // MARK: - Public Variables
    public LevelProvider levelProvider;
    public TileProvider tileProvider;
    public int width;
    public int height;
    public int moveCount;
    public int score;
    public GameObject[,] allTiles;
    public bool swipeApplied = false;

    // Start is called before the first frame update
    void Start()
    {
        FetchLevelInfo(1);
        gameBoard = new BackgroundTile[width, height];
        allTiles = new GameObject[width, height];
        ConfigureTiles();
    }

    void Update()
    {
        if (matchFound)
        {
            Debug.Log(matchedRows[0]);
        }
    }

    // MARK: - Private Functions
    private void FetchLevelInfo(int levelNo)
    {
        List<int> levelInfo = levelProvider.RequestLevelInfo(levelNo);
        width = levelInfo[0];
        height = levelInfo[1];
        moveCount = levelInfo[2];
        grid = levelProvider.GetGrid();
    }

    private void ConfigureTiles()
    {
        int indexForGrid = 0;
        for (int i = 0; i < width; i ++)
        {
            for (int j = 0; j < height; j ++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = tileProvider.DeliverTile("background", tempPosition);
                backgroundTile.transform.parent = this.transform;

                GameObject tile = tileProvider.DeliverTile(grid[indexForGrid], tempPosition);
                tile.transform.parent = this.transform;

                allTiles[i, j] = tile;
                indexForGrid++;
            }
        }
    }

    private void DisableMovement(int row)
    {
        for (int i = 0; i < width; i ++)
        {
            Vector2 tempPosition = new Vector2(i, row);
            GameObject tile = allTiles[i, row];
            tile.GetComponent<Tile>().canSwipe = false;
            GameObject completedTile = tileProvider.DeliverTile("background", tempPosition);
            tile = completedTile;
        }
    }

    private void AddScore(string color)
    {
        if (color == "y")
        {
            score += 250;
        }
        else if (color == "r")
        {
            score += 100;
        }
        else if (color == "g")
        {
            score += 150;
        }
        else if (color == "b")
        {
            score += 200;
        }
        else
        {
            score += 0;
        }

        Debug.Log("SCORE IS: " + score);
    }

    // MARK: - Public Functions
    public void FindMatches(int[] forRow)
    {
        for (int i = 0; i < forRow.Length; i++) 
        {
            List<string> itemList = new List<string>();
            for (int j = 0; j < width; j++)
            {
                GameObject itemToSearch = allTiles[j, forRow[i]];
                itemList.Add(itemToSearch.GetComponent<Tile>().color);
            }
            itemList = itemList.Distinct().ToList();
            if (itemList.Count() == 1)
            {
                if (!(matchedRows.Contains(forRow[i])))
                {
                    Debug.Log("ITS A MATCH AT ROW: " + forRow[i]);
                    matchFound = true;
                    matchedRows.Add(forRow[i]);
                    DisableMovement(forRow[i]);
                    AddScore(itemList[0]);
                }
            }
        }
    }
}
