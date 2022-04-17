﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class LevelSelectionMenu : MonoBehaviour
{
    private List<MenuCustomButton> levelButtons = new List<MenuCustomButton>();
    public int levelCount;
    public LevelProvider levelProvider;

    // Start is called before the first frame update
    void Start()
    {
        PlaceBanner();
        levelCount = 25;
        CreateButtons();
    }

    void Update()
    {
        CheckForInput();
    }

    private void PlaceBanner()
    {
        Vector2 top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height - 40));

        LevelBanner banner = Instantiate(Resources.Load<LevelBanner>("LevelBanner"), top, Quaternion.identity);
        banner.ChangeName("Levels");
        banner.transform.SetParent(this.transform);
    }

    private void CreateButtons()
    {
        Vector2 previousButtonLocation = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height - 160));

        for (int i = 0; i < levelCount; i++)
        {
            MenuCustomButton newButton;
            Vector2 tempPosition = previousButtonLocation;
            newButton = ButtonProvider.DeliverCustomButtonForMenu(tempPosition);
            previousButtonLocation = previousButtonLocation - new Vector2(0, (float)3.5);
            LevelScoreInfo loadedData = DataSaver.LoadLevelInfo(i);

            newButton.transform.SetParent(this.transform);
            newButton.ChangeText("Level: " + (i + 1) + "\nHighscore: " + loadedData.highScore);
            newButton.index = i;

            levelButtons.Add(newButton);
        }
    }

    private void ButtonOnClick(int index)
    {
        Debug.Log("IM HEREEE: " + index);

        if (index == 0)
        {
            DataSaver.SaveLevelInfo(index, 0, true);
        }

        PlayerPrefs.SetInt("selectedLevel", index);

        SceneProvider.GetInstance().LoadLevelScene();
    }



    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            Debug.Log(pos);

            if (hitInfo)
            {
                GameObject colliderHit = hitInfo.transform.gameObject as GameObject;

                if (colliderHit != null)
                {
                    foreach (MenuCustomButton button in levelButtons)
                    {
                        if (colliderHit.name == button.GetComponent<BoxCollider2D>().name)
                        {
                            Debug.Log("Hit button with index: " + button.index);
                            ButtonOnClick(button.index);
                        }
                    }
                }
            }
        }
    }
}
