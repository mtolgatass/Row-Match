using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryScene : MonoBehaviour
{
    public ButtonProvider buttonProvider;
    public SceneProvider sceneProvider;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        RequestButton();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RequestButton()
    {
        GameObject newButton;
        newButton = buttonProvider.DeliverButton(canvas.transform);
        newButton.GetComponent<Button>().onClick.AddListener(() => { ButtonOnClick(); });
    }

    private void ButtonOnClick()
    {
        sceneProvider.LoadMainScene();
    }
}
