using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public sealed class CustomButton : MonoBehaviour
{
    void Update()
    {
        CheckForInput();
    }

    public int index = -1;
    public Action<int> ButtonComponentSelectEvent;

    public void ChangeText(string text)
    {
        TextMeshPro[] tmPro = this.GetComponentsInChildren<TextMeshPro>();
        tmPro[0].text = text;
    }

    private void CheckForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if (hitInfo)
            {
                Debug.Log(hitInfo.transform.gameObject.name);
                ButtonComponentSelectEvent(index);
            }
        }
    }
}
