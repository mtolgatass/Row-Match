using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class LevelProvider : MonoBehaviour
{
    public TextAsset[] levels;

    private List<string> grid = new List<string>();
    private int moveCount;
    private int width;
    private int height;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<int> RequestLevelInfo(int levelNo)
    {
        List<int> returnList = new List<int>();
        string theWholeFileAsOneLongString = levels[levelNo].text;
        List<string> eachLine = new List<string>();
        eachLine.AddRange(
                    theWholeFileAsOneLongString.Split("\n"[0]));

        returnList.Add(GetBoardWidth(eachLine[1]));
        returnList.Add(GetBoardHeight(eachLine[2]));
        returnList.Add(GetMoveCount(eachLine[3]));
        GetTilesByOrder(eachLine[4]);

        return returnList;
    }

    public List<string> GetGrid()
    {
        return grid;
    }

    private int GetBoardWidth(string fromLine)
    {
        string[] line = Regex.Split(fromLine, "grid_width: ");
        width = Int32.Parse(line[1]);
        return width;
    }

    private int GetBoardHeight(string fromLine)
    {
        string[] line = Regex.Split(fromLine, "grid_height: ");
        height = Int32.Parse(line[1]);
        return height;
    }

    private int GetMoveCount(string fromLine)
    {
        string[] line = Regex.Split(fromLine, "move_count: ");
        moveCount = Int32.Parse(line[1]);
        return moveCount;
    }

    private void GetTilesByOrder(string fromLine)
    {
        List<string> returnList = new List<string>();
        string[] line = Regex.Split(fromLine, "grid: ");
        string[] colors = Regex.Split(line[1], ",");

        for (int i = 0; i < colors.Length; i++)
        {
            grid.Add(colors[i]);
        }
    }
}
