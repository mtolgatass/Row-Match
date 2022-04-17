using TMPro;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

public sealed class EntryScene : MonoBehaviour
{
    public string[] levelTypes = { "A", "B" };
    public static int[] levelCounts = { 15, 10 };
    private int downloadedLevelCount = 0;
    private int savedLevelInfoCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        RequestButton();
        StartDownloadingLevels();
    }

    void Update()
    {
        CheckForInput();
    }

    private void RequestButton()
    {
        CustomButton newButton;
        Vector2 center = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        newButton = ButtonProvider.DeliverCustomButton(center);
        newButton.transform.SetParent(this.transform);
        newButton.ChangeText("Levels");
    }

    public static void ButtonOnClick()
    {
        SceneProvider.GetInstance().LoadMainScene();
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

                LevelScoreInfo levelInfo = new LevelScoreInfo();
                levelInfo.levelNo = savedLevelInfoCount;
                DataSaver.SaveLevelInfo(savedLevelInfoCount, 0, false);
                savedLevelInfoCount++;
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

    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            Debug.Log(pos);
            Debug.Log(hitInfo);

            if (hitInfo)
            {
                Debug.Log("IM HERE");
                ButtonOnClick();
            }
        }
    }
}
