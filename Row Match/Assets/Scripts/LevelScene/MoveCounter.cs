using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Debug = UnityEngine.Debug;

public sealed class MoveCounter : MonoBehaviour
{
    // MARK: - Public Variables
    public Text text;
    public float duration;
    public GameObject outOfMovesText;

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
        if (currentMoveCount != 0)
        {
            Debug.Log("GAME OVER");
            ShowOutOfMovesText();
        }
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

    private void ShowOutOfMovesText()
    {
        Vector2 position = new Vector2(0, 0);
        GameObject text = Instantiate(outOfMovesText, position, Quaternion.identity);
        Vector3 scale = this.transform.localScale;
        text.transform.SetParent(this.transform.parent);
        text.transform.localScale = Vector2.zero;
        text.transform.DOScale(scale, 0.8f);
        //text.GetComponent<OutOfMovesCanvas>().transform.height = 80;
        //text.GetComponent<OutOfMovesCanvas>().transform.width = 300;
        //text.GetComponent<OutOfMovesCanvas>().transform.DOMoveY(5f, 5);
    }
}
