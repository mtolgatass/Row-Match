using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntryScene : MonoBehaviour
{
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
        CustomButton newButton;
        newButton = ButtonProvider.DeliverCustomButton(this.transform);
        newButton.ChangeText("Levels");
        newButton.onClick.AddListener(() => { ButtonOnClick(); });
    }

    private void ButtonOnClick()
    {
        Debug.Log("IM HEREEE");
        SceneProvider.GetInstance().LoadMainScene();
    }
}
