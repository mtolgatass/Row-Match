using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelBanner : MonoBehaviour
{
    public void ChangeName(string text)
    {
        TextMeshPro[] tmPro = this.GetComponentsInChildren<TextMeshPro>();
        tmPro[0].text = text;
    }
}
