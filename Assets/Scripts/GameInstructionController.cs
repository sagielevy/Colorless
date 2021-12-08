using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameInstructionController : MonoBehaviour
{
    [SerializeField] private TextDisplayController instructions;
    [SerializeField] private float instructionsDuration = 15f;
    [SerializeField] private float instructionsFadeDuration = 2f;

    private Coroutine timer;

    private void Start()
    {
        timer = StartCoroutine(StartTimeoutCounter());
    }

    private IEnumerator<WaitForSeconds> StartTimeoutCounter()
    {
        yield return new WaitForSeconds(instructionsDuration);

        timer = null;
        instructions.FadeOut(instructionsFadeDuration);
    }

    public void TurnOnGameInstructions()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }

        instructions.SetDefaultOpacity(true);
        timer = StartCoroutine(StartTimeoutCounter());
    }

    public void TurnOffInstructions()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
            instructions.FadeOut(instructionsFadeDuration);
        }
    }
}
