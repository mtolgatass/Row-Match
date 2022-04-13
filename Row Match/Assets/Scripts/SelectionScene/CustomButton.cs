using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public int index;
    public Text levelLabel;
    //public Text highScoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupButton()
    {

    }

    private void OnMouseDown()
    {
        print("Button with index: " + index);
    }
}
