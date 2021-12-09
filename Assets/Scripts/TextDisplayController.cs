using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextDisplayController : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    private const float defaultBGOpacity = 0.4f;
    private const float defaultTextOpacity = 1f;

    public void FadeOut(float duration)
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, defaultBGOpacity);
        background.DOKill();
        background.DOFade(0, duration).OnComplete(() => background.raycastTarget = false);

        text.color = new Color(text.color.r, text.color.g, text.color.b, defaultTextOpacity);
        text.DOKill();
        text.DOFade(0, duration);

        background.raycastTarget = false;
        text.raycastTarget = false;
    }

    public void SetDefaultOpacity(bool shouldBeRaycastTarget)
    {
        background.DOKill();
        text.DOKill();

        background.color = new Color(background.color.r, background.color.g, background.color.b, defaultBGOpacity);
        text.color = new Color(text.color.r, text.color.g, text.color.b, defaultTextOpacity);

        if (shouldBeRaycastTarget)
        {
            background.raycastTarget = true;
            text.raycastTarget = true;
        }
    }
}
