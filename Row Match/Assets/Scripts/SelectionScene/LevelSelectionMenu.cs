using TMPro;
using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public sealed class LevelSelectionMenu : MonoBehaviour
{
    private List<MenuCustomButton> levelButtons = new List<MenuCustomButton>();
    public int levelCount;
    public LevelProvider levelProvider;

    // Start is called before the first frame update
    void Start()
    {
        SetLevelCount();
        PlaceBanner();
        CreateButtons();
    }

    void Update()
    {
        CheckForInput();
    }

    private void SetLevelCount()
    {
        int levelCount = PlayerPrefs.GetInt("downloadedLevelCount");
        if (levelCount == 0)
        {
            this.levelCount = 10;
        }
        else
        {
            this.levelCount = levelCount;
        }
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
            previousButtonLocation = previousButtonLocation - new Vector2(0, (float)2.75);
            LevelScoreInfo loadedData = DataSaver.LoadLevelInfo(i);

            List<int> levelInfo = levelProvider.RequestLevelInfo(i);
            int moveCount = levelInfo[2];

            newButton.transform.SetParent(this.transform);
            newButton.ChangeText("Level: " + (i + 1) + " - " + moveCount + " Moves" + "\nHighscore: " + loadedData.highScore);
            newButton.index = i;

            if (!loadedData.isEnabled && loadedData.levelNo != 0)
            {
                newButton.ChangeColorToDisabled();
            }

            levelButtons.Add(newButton);
        }
    }

    private void ButtonOnClick(int index)
    {
        LevelScoreInfo levelInfo = DataSaver.LoadLevelInfo(index);

        if (levelInfo.isEnabled)
        {
            PlayerPrefs.SetInt("selectedLevel", index);
            SceneProvider.GetInstance().LoadLevelScene();
        }
        else
        {
            ShowLockedError();
        }
    }


    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if (hitInfo)
            {
                GameObject colliderHit = hitInfo.transform.gameObject as GameObject;

                if (colliderHit != null)
                {
                    foreach (MenuCustomButton button in levelButtons)
                    {
                        if (colliderHit.name == button.GetComponent<BoxCollider2D>().name)
                        {
                            ButtonOnClick(button.index);
                        }
                    }
                }
            }
        }
    }

    private void ShowLockedError()
    {
        Vector2 centerBottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0));
        WarningBanner banner = Instantiate(Resources.Load<WarningBanner>("WarningBanner"), centerBottom, Quaternion.identity);
        banner.ChangeText("Play Previous Level First");
        banner.transform.SetParent(this.transform);

        banner.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        banner.GetComponentInChildren<TextMeshPro>().sortingOrder = 2;

        Vector2 center = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        banner.transform
            .DOLocalMoveY(Screen.height / 8, 3f)
            .SetEase(Ease.OutBounce)
            .OnStepComplete(() => { banner.DestroyBanner(); });

    }
}
