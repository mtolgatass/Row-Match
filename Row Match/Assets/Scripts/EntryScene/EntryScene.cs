using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
        newButton.ButtonComponentSelectEvent = (int index) =>
        {
            ButtonOnClick(index);
        };
    }

    public static void ButtonOnClick(int index)
    {
        Debug.Log("IM HEREEE: " + index);
        SceneProvider.GetInstance().LoadMainScene();
    }


}
