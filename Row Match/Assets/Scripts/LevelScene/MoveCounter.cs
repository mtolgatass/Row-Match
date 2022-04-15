using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveCounter : MonoBehaviour
{
    // MARK: - Public Variables
    public Text text;
    public float duration;

    // MARK: - Private Variables
    public int currentMoveCount = 0;
    private string currentMoveCountText = "";


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // MARK: - Public Functions
    public void SetMoveCount(int count)
    {
        currentMoveCount = count;
        currentMoveCountText = currentMoveCount.ToString();
        AddDecreaseAnimation();
    }

    public void DecreaseMoveCount()
    {
        currentMoveCount -= 1;
        currentMoveCountText = currentMoveCount.ToString();
        AddDecreaseAnimation();
    }

    // MARK: - Private Functions
    private void AddDecreaseAnimation()
    {
        string txt = "";
        DOTween.To(
            () => txt,
            x => txt = x,
            currentMoveCountText,
            duration
            ).OnUpdate(() => text.text = txt);
    }
}
