using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCustomButton : MonoBehaviour
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

public void ChangeColorToDisabled()
{
SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();
sprites[0].color = new Color (0.5f, 0.5f, 0.5f, 1);
}
}
