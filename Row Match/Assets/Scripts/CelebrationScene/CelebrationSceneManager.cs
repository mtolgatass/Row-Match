using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CelebrationSceneManager : MonoBehaviour
{
    private CelebrationPopup celebrationPopup;

    // Start is called before the first frame update
    void Start()
    {
        ShowCelebrationView();
        AnimateStar();
        AnimateText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowCelebrationView()
    {
        Vector2 center = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0));
        celebrationPopup = Instantiate(Resources.Load<CelebrationPopup>("CelebrationPopup"), center, Quaternion.identity);
        celebrationPopup.transform.SetParent(this.transform);
    }

    private void ShowInitialScene()
    {
        SceneProvider.GetInstance().LoadInitialScene();
    }

    private void AnimateStar()
    {
        Sequence starAnimationSequence = DOTween.Sequence();
        Transform starIcon = celebrationPopup.transform.Find("StarSprite");


        starIcon.localScale = Vector3.one;

        starAnimationSequence
            .Join(starIcon.DOScale(10, 1).SetEase(Ease.OutBack))
            .Join(starIcon.DORotate(new Vector3(0, 0, 180), 1))
            .SetLoops(10, LoopType.Yoyo)
            .OnComplete(ShowInitialScene);
    }

    private void AnimateText()
    {
        Sequence textAnimationSequence = DOTween.Sequence();
        Transform text = celebrationPopup.transform.Find("TMP");

        text.localScale = Vector3.zero;

        textAnimationSequence
            .Join(text.GetComponent<Renderer>().material.DOFade(0, 1))
            .Join(text.DOScale(1, 1).SetEase(Ease.OutBack))
            .SetLoops(10, LoopType.Yoyo);
    }
}
