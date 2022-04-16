using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomButton : MonoBehaviour
{
    private CustomButton button;
    // Start is called before the first frame update
    public int index = -1;

    public void ChangeText(string text)
    {
        TextMeshPro[] tmPro = this.GetComponentsInChildren<TextMeshPro>();
        tmPro[0].text = text;
    }
}
