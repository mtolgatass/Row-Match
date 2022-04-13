using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    public LevelProvider levelProvider;
    public MenuItemProvider menuItemProvider;

    private GameObject[] levelButtons;
    public int levelCount;

    // Start is called before the first frame update
    void Start()
    {
        levelCount = levelProvider.levels.Length;
        levelButtons = new GameObject[levelCount];
        CreateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateButtons()
    {
        for (int i = 0; i < levelCount; i ++)
        {
            Vector2 tempPosition = new Vector2(3, i * 2);
            GameObject levelButton = menuItemProvider.DeliverMenuItem(tempPosition, i, 650);
            levelButton.GetComponent<CustomButton>().index = i;
            levelButtons[i] = levelButton;
        }
    }
}
