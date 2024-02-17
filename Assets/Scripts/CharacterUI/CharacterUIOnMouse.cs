using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIOnMouse : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] CharacterUIOnClick CharacterUIOnClick;

    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
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
            // UI�̔w�i�F���D�F�ɕύX
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
            // UI�̔w�i�F�����ɖ߂�
            ChangeBackgroundColor(originalColor);
        }
    }

    //�w�i�F��ύX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
