using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemProvider : MonoBehaviour
{
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject DeliverMenuItem(Vector2 position, int levelCount, int highScore)
    {
        GameObject levelButton = Instantiate(buttonPrefab, position, Quaternion.identity);

        return levelButton;
    }
}
