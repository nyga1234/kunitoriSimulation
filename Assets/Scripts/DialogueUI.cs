using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] Text dialogueText;

    private bool isDialogueVisible = false;

    public void ShowLeaveInfluenceUI(CharacterController character)
    {
        // �_�C�A���O��\�����鏈��
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.name + "�����͂�����܂���";
    }

    public void ShowEnterInfluenceUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�̗p����܂���";
    }

    public void ShowSuccessAppointmentUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�o�p�ɐ������܂���";
    }

    public void ShowEmployedUI(CharacterController lordCharacter)
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = lordCharacter.characterModel.name + "�R�։������܂���";
    }

    public void ShowSuccessBanishmentUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�Ǖ��ɐ������܂���";
    }

    public void ShowSuccessVagabondUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "���͂�����܂���";
    }

    public void ShowElavationRankUI(CharacterController character)
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.rank + "�ɏ��i���܂���";
    }

    public void ShowDemotionRankUI(CharacterController character)
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.rank + "�ɍ~�i���܂���";
    }

    public void ShowAttackedUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�h�q������I�����Ă�������";
    }

    public void ShowBattleOrderUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�퓬���J�n���܂�";
    }

    public void HideDialogueUI()
    {
        // �_�C�A���O���\���ɂ��鏈��
        this.gameObject.SetActive(false);
        isDialogueVisible = false;
    }

    public bool IsDialogueVisible()
    {
        return isDialogueVisible;
    }
}
