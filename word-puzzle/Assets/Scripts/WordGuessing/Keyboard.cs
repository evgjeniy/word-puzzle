using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WordGuessing;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private LettersContainer container;
    [SerializeField] private Button backSpaceButton;

    private Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        foreach (var button in _buttons)
        {
            button.onClick.AddListener(button == backSpaceButton 
                ? () => container.OnBackSpaceEnter(ActivateButtonByLetter)
                : () => container.SetLetter(button.name[0], DeactivateButtonByLetter)
            );
        }
    }

    private void ActivateButtonByLetter(char letter)
    {
        var button = _buttons.FirstOrDefault(b => b.name[0] == letter);
        if (button) button.interactable = true;
    }

    private void DeactivateButtonByLetter(char letter)
    {
        var button = _buttons.FirstOrDefault(b => b.name[0] == letter);
        if (button) button.interactable = false;
    }

    public void RecalculateKeyboardButtons(string wordToGuess)
    {
        foreach (var button in _buttons)
            button.interactable = wordToGuess.Contains(button.name);

        backSpaceButton.interactable = true;
    }
}