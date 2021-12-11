using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
public class TextAnimationController : MonoBehaviour
{
    private float defaultXLocalPosition;
    private Image image;
    private Coroutine timer;
    private Sequence fadeSequence;
    private Sequence moveSequence;

    [SerializeField] private float defaultImageOpacity = 0.8f;
    [SerializeField] private float finalXPositionRelativeToDefault;
    [SerializeField] private float halfwayAnimationDuration = 1f;
    [SerializeField] private float waitDurationBeforeAnimationStart = 25f;

    private void Awake()
    {
        image = GetComponent<Image>();
        defaultXLocalPosition = image.rectTransform.localPosition.x;
    }

    private void OnEnable()
    {
        StopAnimating();
        timer = StartCoroutine(StartAnimating());
    }

    private void OnDisable()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
    }

    private IEnumerator<WaitForSeconds> StartAnimating()
    {
        yield return new WaitForSeconds(waitDurationBeforeAnimationStart);

        Animate();
    }

    private void Animate()
    {
        fadeSequence = DOTween.Sequence().
            Append(image.DOFade(0.2f, halfwayAnimationDuration)).
            Append(image.DOFade(defaultImageOpacity, halfwayAnimationDuration)).
            SetLoops(-1);

        moveSequence = DOTween.Sequence().
            Append(image.rectTransform.DOLocalMoveX(defaultXLocalPosition + finalXPositionRelativeToDefault, halfwayAnimationDuration)).
            Append(image.rectTransform.DOLocalMoveX(defaultXLocalPosition, halfwayAnimationDuration)).
            SetLoops(-1);
    }

    private void StopAnimating()
    {
        if (fadeSequence != null) { fadeSequence.Kill(); }
        if (moveSequence != null) { moveSequence.Kill(); }
        
        image.rectTransform.localPosition = new Vector3(defaultXLocalPosition, image.rectTransform.localPosition.y);
        image.color = new Color(image.color.r, image.color.g, image.color.b, defaultImageOpacity);
    }
}
