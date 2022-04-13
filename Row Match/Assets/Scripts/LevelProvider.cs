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
    public string[] levelTypes;
    public int[] levelCounts;

    // MARK: - Private Variables
    private List<string> grid = new List<string>();
    private int moveCount;
    private int width;
    private int height;
    private int downloadedLevelCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelTypes.Length; i ++)
        {
            for (int j = 1; j < levelCounts[i] + 1; j ++)
            {
                StartCoroutine(DownloadLevel(levelTypes[i], j));
            }
        }
    }

    IEnumerator DownloadLevel(string type, int levelIndex)
    {
        string filePath = Application.persistentDataPath + "/Level" + downloadedLevelCount + ".txt";
        downloadedLevelCount++;

        string downloadURL = "https://row-match.s3.amazonaws.com/levels/RM_" + type + levelIndex.ToString();

        if (!File.Exists(filePath))
        {
            Debug.Log("IM DOWNLOADIIIIINGGGGG");
            UnityWebRequest www = new UnityWebRequest(downloadURL);
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                SaveLevel(results, filePath);
            }
        }
        else
        {
            Debug.Log("IM NOOOOOT DOWNLOADIIIIINGGGGG");
        }
    }

    private void SaveLevel(byte[] levelInfo, string path)
    {
        using (FileStream file = File.Create(path))
        {
            new BinaryFormatter().Serialize(file, levelInfo);
        }
    }

    // MARK: - Public Functions
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

    private void RequestLevelInfoFromPersistantData()
    {
        //TODO
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
}
