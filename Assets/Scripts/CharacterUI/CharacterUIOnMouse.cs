using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIOnMouse : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] CharacterUIOnClick CharacterUIOnClick;

    private Color originalColor; // ³ÌwiFðÛ·éÏ

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ³ÌwiFðÛ
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
            // UIÌwiFðDFÉÏX
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
            // UIÌwiFð³Éß·
            ChangeBackgroundColor(originalColor);
        }
    }

    //wiFðÏX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
