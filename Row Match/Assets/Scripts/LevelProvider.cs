using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelProvider : MonoBehaviour
{
    // MARK: - Public Variables
    public TextAsset[] levels;

    // MARK: - Private Variables
    private List<string> grid = new List<string>();
    private int moveCount;
    private int width;
    private int height;
    private Dictionary<int, int> levelScores = new Dictionary<int, int>();

    public List<int> RequestLevelInfo(int levelNo)
    {
        List<int> returnList = new List<int>();
        List<string> eachLine = new List<string>();

        string levelInfo = RequestLevelInfoFromPersistantData(levelNo);
        if (levelInfo == "")
        {
            levelInfo = levels[levelNo].text;
        }
        eachLine.AddRange(
                    levelInfo.Split("\n"[0]));

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

    // MARK: - Private Functions
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

    private string RequestLevelInfoFromPersistantData(int level)
    {
        string filePath = Application.persistentDataPath + "/Level" + level + ".txt";

        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            String data = formatter.Deserialize(stream) as String;
            stream.Close();
            return data;
        }
        else
        {
            return "";
        }
    }
}