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
        SoundManager.instance.PlayDialogueSE();
        // �_�C�A���O��\�����鏈��
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.name + "�����͂�����܂���";
    }

    public void ShowEnterInfluenceUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�̗p����܂���";
    }

    public void ShowSuccessAppointmentUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�o�p�ɐ������܂���";
    }

    public void ShowEmployedUI(CharacterController lordCharacter)
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = lordCharacter.characterModel.name + "�R�։������܂���";
    }

    public void ShowSuccessBanishmentUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�Ǖ��ɐ������܂���";
    }

    public void ShowSuccessVagabondUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "���͂�����܂���";
    }

    public void ShowElavationRankUI(CharacterController character)
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.rank + "�ɏ��i���܂���";
    }

    public void ShowDemotionRankUI(CharacterController character)
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.rank + "�ɍ~�i���܂���";
    }

    public void ShowAttackedUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�h�q������I�����Ă�������";
    }

    public void ShowBattleOrderUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "�퓬���J�n���܂�";
    }

    public void HideDialogueUI()
    {
        SoundManager.instance.PlayClickSE();
        // �_�C�A���O���\���ɂ��鏈��
        this.gameObject.SetActive(false);
        isDialogueVisible = false;
    }

    public bool IsDialogueVisible()
    {
        return isDialogueVisible;
    }
}
