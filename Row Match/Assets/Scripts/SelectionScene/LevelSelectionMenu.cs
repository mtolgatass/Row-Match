using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    private List<GameObject> levelButtons = new List<GameObject>();
    public int levelCount;

    // Start is called before the first frame update
    void Start()
    {
        levelCount = LevelProvider.levels.Length;
        CreateButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateButtons()
    {
        for (int i = 0; i < levelCount; i++)
        {
            //Vector2 tempPosition = new Vector2(3, i * 2);
            //GameObject levelButton = buttonProvider.DeliverBetterButton(tempPosition);
            //levelButton.onClick.AddListener(() => { ButtonOnClick(i); });


            //TextMeshPro tmpTextComponent = levelButton.GetComponentInChildren<TextMeshPro>();
            //tmpTextComponent.text = "Level: " + i + "\n Highscore: ";

            //levelButtons.Add(levelButton);
        }
    }

    private void ButtonOnClick(int index)
    {
        Debug.Log("IM HEREEE");
        SceneProvider.GetInstance().LoadMainScene();
    }
}
