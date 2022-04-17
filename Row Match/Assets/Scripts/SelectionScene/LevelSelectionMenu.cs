using TMPro;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public sealed class LevelSelectionMenu : MonoBehaviour
{
    // MARK: - Public Variables
    public int levelCount;
    public LevelProvider levelProvider;

    // MARK: - Private Variables
    private List<MenuCustomButton> levelButtons = new List<MenuCustomButton>();
    private Vector2 firstTouchCoordinates;
    private Vector2 finalTouchCoordinates;
    private float swipeAngle = 0;

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

    // MARK: - Private Functions
    private void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchCoordinates.y - firstTouchCoordinates.y, finalTouchCoordinates.x - firstTouchCoordinates.x) * 180 / Mathf.PI;
    }

    private void MoveCameraToDirection()
    {
        if (swipeAngle > 0 && swipeAngle <= 180)
        {
            SwipeUpAction();
        }
        else if (swipeAngle < 0 && swipeAngle >= -180)
        {
            SwipeDownAction();
        }
    }

    private void SwipeUpAction()
    {
        Vector3 position = new Vector3(0, (firstTouchCoordinates.y - finalTouchCoordinates.y), 0);
        Camera.main.transform.Translate(position);
    }

    private void SwipeDownAction()
    {
        Vector3 position = new Vector3(0, -(finalTouchCoordinates.y - firstTouchCoordinates.y), 0);
        Camera.main.transform.Translate(position);
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
            List<int> levelInfo = levelProvider.RequestLevelInfo(i);
            if (levelInfo.Count <= 1)
            {
                return;
            }

            MenuCustomButton newButton;
            Vector2 tempPosition = previousButtonLocation;
            newButton = ButtonProvider.DeliverCustomButtonForMenu(tempPosition);
            previousButtonLocation = previousButtonLocation - new Vector2(0, (float)2.75);
            LevelScoreInfo loadedData = DataSaver.LoadLevelInfo(i);

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
            firstTouchCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        else if (Input.GetMouseButtonUp(0))
        {
            finalTouchCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
            MoveCameraToDirection();
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
        Debug.Log("Camera Y is: " + Camera.main.transform.position.y);

        banner.transform
            .DOLocalMoveY(3 - Camera.main.transform.position.y, 3f)
            .SetEase(Ease.OutBounce)
            .OnStepComplete(() => { banner.DestroyBanner(); });

    }
}
