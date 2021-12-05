using UnityEngine;
using System.Collections;

public class GameInstructionController : MonoBehaviour
{
    [SerializeField] private GameObject instructions;

    public void ToggleGameInstructions(bool isOn)
    {
        instructions.SetActive(isOn);
    }
}
