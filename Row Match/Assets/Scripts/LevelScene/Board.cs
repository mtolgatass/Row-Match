using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
using System;

public sealed class Board : MonoBehaviour
{
    // MARK: - Private Variables
    private bool matchFound = false;
    private List<int> matchedRows = new List<int>();
    private List<string> matchedColors = new List<string>();
    private List<string> grid = new List<string>();
    private int rowToAnimate;
    private int currentLevel;
    private bool shouldShowCelebration = false;

    // MARK: - Public Variables
    public Camera camera;
    public ScoreCounter scoreCounter;
    public MoveCounter moveCounter;
    public LevelProvider levelProvider;
    public TileProvider tileProvider;
    public float animationDuration;
    public float completeRowAnimationDuration;
    public Ease moveRowUpAnimationEase;
    public Ease completeRowAnimationEase;

    public int width;
    public int height;
    public GameObject[,] allTiles;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("selectedLevel");

        FetchLevelInfo(currentLevel);
        RepositionCamera();
        allTiles = new GameObject[width, height];
        ConfigureTiles();
        scoreCounter.SetInitialScore();
    }

    void Update()
    {
        if (matchFound)
        {
            MoveMatchedRowUp();
            matchFound = false;
        }

        if (moveCounter.currentMoveCount <= 0)
        {
            DisableSwiping();
            SaveLevelInfo();
            Invoke("QuitLevel", 5);

        }
    }

    // MARK: - Private Functions
    private void DisableSwiping()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                allTiles[i, j].GetComponent<Tile>().canSwipe = false;
    }

    private void SaveLevelInfo()
    {
        LevelScoreInfo currentLevelInfo = DataSaver.LoadLevelInfo(currentLevel);
        if (scoreCounter.currentScore > currentLevelInfo.highScore)
        {
            DataSaver.SaveLevelInfo(currentLevel, scoreCounter.currentScore, true);
            shouldShowCelebration = true;
            Debug.Log("HIGH SCORE!!!");
        }
        else
        {
            shouldShowCelebration = false;
        }

        LevelScoreInfo nextLevelInfo = DataSaver.LoadLevelInfo(currentLevel + 1);
        DataSaver.SaveLevelInfo(nextLevelInfo.levelNo, nextLevelInfo.highScore, true);
    }

    private void QuitLevel()
    {
        if (shouldShowCelebration)
        {
            SceneProvider.GetInstance().LoadCelebrationScene();
        }
        else
        {
            SceneProvider.GetInstance().LoadInitialScene();
        }
    }

    private void FetchLevelInfo(int levelNo)
    {
        List<int> levelInfo = levelProvider.RequestLevelInfo(levelNo);
        width = levelInfo[0];
        height = levelInfo[1];
        moveCounter.SetMoveCount(levelInfo[2]);
        grid = levelProvider.GetGrid();
    }

    private void ConfigureTiles()
    {
        int indexForGrid = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject tile = tileProvider.DeliverTile(grid[indexForGrid], tempPosition);
                tile.transform.parent = this.transform;

                allTiles[i, j] = tile;
                indexForGrid++;
            }
        }
    }

    private void MoveMatchedRowUp()
    {
        for (int i = 0; i < width; i++)
        {
            GameObject tile = allTiles[i, rowToAnimate];
            tile.GetComponent<Tile>().sphereRenderer.sortingOrder = 2;
            tile.GetComponent<Tile>().transform
                .DOMoveY(10f, animationDuration)
                .SetEase(moveRowUpAnimationEase)
                .OnStepComplete(() => { Destroy(tile); });
            PlaceCompleteRow(i, rowToAnimate);
        }
    }

    private void PlaceCompleteRow(int row, int column)
    {
        Vector2 tempPosition = new Vector2(row, column);
        GameObject tileToDestroy = allTiles[row, column];
        GameObject completeTile = tileProvider.DeliverTile("complete", tempPosition);
        completeTile.transform.parent = this.transform;
        completeTile.GetComponent<Tile>().canSwipe = false;
        allTiles[row, column] = completeTile;

        float destination = column + 1f;
        Vector2 strength = new Vector2(1, 0);

        completeTile.GetComponent<Tile>().transform
            .DOLocalMoveY(destination, completeRowAnimationDuration)
            .SetEase(completeRowAnimationEase);
    }

    void RepositionCamera()
    {
        float x = (float)((double)width - 1) / 2;
        Vector3 tempPosition = new Vector3(x, height - 2, -10);
        Debug.Log(tempPosition);
        camera.transform.position = tempPosition;
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
                    matchFound = true;
                    matchedRows.Add(forRow[i]);
                    scoreCounter.IncreaseScore(itemList[0], width);
                    rowToAnimate = forRow[i];
                }
            }
        }
    }
}
