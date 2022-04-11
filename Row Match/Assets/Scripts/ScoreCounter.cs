using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int currentScore = 0;

    public Transform textTransform;
    public Text text;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreaseScore(string color)
    {
        int increaseScoreBy = 0;
        if (color == "y")
        {
            increaseScoreBy = 250;
        }
        else if (color == "r")
        {
            increaseScoreBy = 100;
        }
        else if (color == "g")
        {
            increaseScoreBy = 150;
        }
        else if (color == "b")
        {
            increaseScoreBy = 200;
        }
        else
        {
            increaseScoreBy = 0;
        }

        UpdateScore(increaseScoreBy);
    }

    private void UpdateScore(int increaseBy)
    {
        int destinationScore = currentScore + increaseBy;
        if (currentScore != (destinationScore))
        {
            Debug.Log("Current score is: " + currentScore + " Incresing to: " + destinationScore);
            DOVirtual.Int(currentScore, destinationScore, duration,(v)=>text.text = v.ToString());
            currentScore = destinationScore;
        }
    }

}
