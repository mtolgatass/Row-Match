using TMPro;
using System;
using System.IO;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

public sealed class EntryScene : MonoBehaviour
{
    // MARK: - Public Variables
    public string[] levelTypes = { "A", "B" };
    public static int[] levelCounts = { 15, 10 };

    // MARK: - Private Variables
    private int downloadedLevelCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ShowCelebrationPopup();
        RequestButton();
        StartDownloadingLevels();
    }

    void Update()
    {
        CheckForInput();
    }

    // MARK: - Private Functions
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
            for (int j = 0; j < levelCounts[i] + 1; j++)
            {
                StartCoroutine(DownloadLevel(levelTypes[i], j));
            }
        }
    }

    IEnumerator DownloadLevel(string type, int levelIndex)
    {
        string filePath = Application.persistentDataPath + "/Level" + downloadedLevelCount + ".txt";
        downloadedLevelCount++;
        PlayerPrefs.SetInt("downloadedLevelCount", downloadedLevelCount);
        string downloadURL = "https://row-match.s3.amazonaws.com/levels/RM_" + type + levelIndex.ToString();

        if (!File.Exists(filePath))
        {
            bool isFirstLevel = levelIndex == 0;
            DataSaver.SaveLevelInfo(levelIndex, 0, isFirstLevel);

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

            if (hitInfo)
            {
                ButtonOnClick();
            }
        }
    }

    private void ShowCelebrationPopup()
    {
        //bool shouldShowCelebration = Convert.ToBoolean(PlayerPrefs.GetInt("shouldShowCelebration"));
        //if (shouldShowCelebration)
        //{
        Vector2 startingPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 center = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0));
        CelebrationPopup popup = Instantiate(Resources.Load<CelebrationPopup>("CelebrationPopup"), startingPoint, Quaternion.identity);
        popup.transform.SetParent(this.transform);

        //popup.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        //popup.GetComponentInChildren<TextMeshPro>().sortingOrder = 2;

        //Vector2 center = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        //popup.transform
        //    .DOLocalMoveY(Screen.height / 8, 3f)
        //    .SetEase(Ease.OutBounce);
        //}
    }
}
