using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public sealed class ScoreCounter : MonoBehaviour
{
    // MARK: - Public Variables
    public int currentScore = 0;
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

    public void SetInitialScore()
    {
        IncreaseScore("", 0);
    }

    // MARK: - Public Functions
    public void IncreaseScore(string color, int count)
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
        increaseScoreBy *= count;

        UpdateScore(increaseScoreBy);
    }

    // MARK: - Private Functions
    private void UpdateScore(int increaseBy)
    {
        int destinationScore = currentScore + increaseBy;
        if (currentScore != (destinationScore))
        {
            DOVirtual.Int(currentScore, destinationScore, duration, (v) => text.text = v.ToString());
            currentScore = destinationScore;
        }
        else
        {
            text.text = currentScore.ToString();
            currentScore = increaseBy;
        }
    }

}
