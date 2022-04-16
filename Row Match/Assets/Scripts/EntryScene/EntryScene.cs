using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        newButton = buttonProvider.DeliverBetterButton(canvas.transform);

        Button buttonComponent = newButton.GetComponentInChildren<Button>();
        buttonComponent.onClick.AddListener(() => { ButtonOnClick(); });

        TextMeshPro tmpTextComponent = newButton.GetComponentInChildren<TextMeshPro>();
        tmpTextComponent.text = "START";
    }

    private void ButtonOnClick()
    {
        Debug.Log("IM HEREEE");
        sceneProvider.LoadMainScene();
    }
}
