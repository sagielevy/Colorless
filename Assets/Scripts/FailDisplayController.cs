using UnityEngine;
using UnityEngine.UI;

public class FailDisplayController : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    private const float defaultBGOpacity = 0.4f;
    private const float defaultTextOpacity = 1f;

    public void SetOpacity(float factor)
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, defaultBGOpacity * factor);
        text.color = new Color(text.color.r, text.color.g, text.color.b, defaultTextOpacity * factor);
    }

    public void DefaultOpacity()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, defaultBGOpacity);
        text.color = new Color(text.color.r, text.color.g, text.color.b, defaultTextOpacity);
    }
}
