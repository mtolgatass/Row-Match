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
    public string[] levelTypes = { "A", "B" };
    public int[] levelCounts = { 15, 10 };

    // MARK: - Private Variables
    private List<string> grid = new List<string>();
    private int moveCount;
    private int width;
    private int height;
    private int downloadedLevelCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartDownloadingLevels();
    }

    IEnumerator DownloadLevel(string type, int levelIndex)
    {
        string filePath = Application.persistentDataPath + "/Level" + downloadedLevelCount + ".txt";
        downloadedLevelCount++;

        string downloadURL = "https://row-match.s3.amazonaws.com/levels/RM_" + type + levelIndex.ToString();

        if (!File.Exists(filePath))
        {
            UnityWebRequest www = new UnityWebRequest(downloadURL);
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string results = www.downloadHandler.text;
                SaveLevel(results, filePath);
            }
        }
    }

    // MARK: - Public Functions
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

    private void StartDownloadingLevels()
    {
        for (int i = 0; i < levelTypes.Length; i++)
        {
            for (int j = 1; j < levelCounts[i] + 1; j++)
            {
                StartCoroutine(DownloadLevel(levelTypes[i], j));
            }
        }
    }

    private void SaveLevel(string levelInfo, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, levelInfo);
        stream.Close();
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
            Debug.Log("THIS IS WHAT I GOT FROM MEMORY" + data);
            return data;
        }
        else
        {
            return "";
        }
    }
}