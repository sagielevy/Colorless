using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class GameInstructionController : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    [SerializeField] private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void ToggleGameInstructions(bool isOn)
    {
        instructions.SetActive(isOn);
    }


    public void ToggleGameInstructions()
    {
        toggle.isOn = !toggle.isOn;

        instructions.SetActive(toggle.isOn);
    }

    public void TurnOffInstructions()
    {
        toggle.isOn = false;
        instructions.SetActive(false);
    }
}
