using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIOnMouse : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] CharacterUIOnClick CharacterUIOnClick;

    private Color originalColor; // Œ³‚Ì”wŒiF‚ğ•Û‚·‚é•Ï”

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // Œ³‚Ì”wŒiF‚ğ•Û
            originalColor = image.color;
        }
    }

    public void OnPointerEnterCharacter()
    {
        characterDetailUI.ShowCharacterDetailUI(characterController);
        //if (characterController != null && characterDetailUI != null)
        //{
        //    characterDetailUI.ShowCharacterDetailUI(characterController);
        //}
            // UI‚Ì”wŒiF‚ğŠDF‚É•ÏX
            ChangeBackgroundColor(Color.gray);
    }

    public void SetCharacterController(CharacterController controller)
    {
        characterController = controller;
    }

    public void OnPointerExit()
    {
        if (CharacterUIOnClick.clickedFlag == false)
        {
            // UI‚Ì”wŒiF‚ğŒ³‚É–ß‚·
            ChangeBackgroundColor(originalColor);
        }
    }

    //”wŒiF‚ğ•ÏX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
