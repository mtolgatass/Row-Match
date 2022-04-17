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
        Vector2 centerBottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0));
        WarningBanner banner = Instantiate(Resources.Load<WarningBanner>("WarningBanner"), centerBottom, Quaternion.identity);
        banner.ChangeText("Out Of Moves");
        banner.transform.SetParent(this.transform);

        Vector2 center = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        banner.transform
            .DOLocalMoveY(Screen.height / 8, 3f)
            .SetEase(Ease.OutBounce);
    }
}
