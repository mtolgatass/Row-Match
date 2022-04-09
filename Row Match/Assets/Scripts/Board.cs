using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    // MARK: - Private Variables
    private BackgroundTile[,] gameBoard;
    private bool matchFound = false;
    private List<int> matchedRows = new List<int>();

    // MARK: - Public Variables
    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[] tiles;
    public GameObject[,] allTiles;
    public bool swipeApplied = false;
    public int[] possibleMatchRows;


    // Start is called before the first frame update
    void Start()
    {
        gameBoard = new BackgroundTile[width, height];
        allTiles = new GameObject[width, height];
        Configure();
    }

    void Update()
    {
        if (matchFound)
        {
            
        }
    }

    // MARK: - Private Functions
    private void Configure()
    {
        for (int i = 0; i < width; i ++)
        {
            for (int j = 0; j < height; j ++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) ;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "Background ( " + i + ", " + j + " )";

                int index = Random.Range(0, tiles.Length);
                GameObject tile = Instantiate(tiles[index], tempPosition, Quaternion.identity);
                tile.transform.parent = this.transform;
                tile.name = "Tile ( " + i + ", " + j + " )";
                allTiles[i, j] = tile;
            }
        }
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
                Debug.Log("ITS A MATCH AT ROW: " + i);
                matchFound = true;
                matchedRows.Add(i);
            }
        }
    }
}
