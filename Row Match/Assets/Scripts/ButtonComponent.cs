using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections;

public class ButtonComponent : Button, IPointerClickHandler
{

    private string _text = "Button Text";
    private int _value = 0;
    private Button baseButton;

    public Action Callback;

    //update button text and stored value
    public string text
    {
        get { return _text; }
        set { GetComponentInChildren<Text>().text = value; _text = value; }
    }

    public int value
    {
        get { return _value; }
        set { _value = value; }
    }

    new void Start()
    {
        //you can expose onClick and assign a listener
        baseButton = GetComponent<Button>();
        if (onClick != null)
            baseButton.onClick = onClick;

        //update label
        baseButton.GetComponentInChildren<Text>().text = text;
    }

    //or you can implement click handler and use 'callback'
    override public void OnPointerClick(PointerEventData ped)
    {
        Debug.Log("clicked button");
        Debug.Log("Label: " + this.text);
        Debug.Log("Value: " + this.value);
        if (Callback != null)
        {
            Callback();
        }
    }
}