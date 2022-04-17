using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public sealed class CustomButton : MonoBehaviour
{
    public int index = -1;
    new private string name = "";

    public void ChangeText(string text)
    {
        TextMeshPro[] tmPro = this.GetComponentsInChildren<TextMeshPro>();
        tmPro[0].text = text;
        BoxCollider2D bc = this.GetComponent<BoxCollider2D>();
        bc.name = text;
        name = text;
    }
}
