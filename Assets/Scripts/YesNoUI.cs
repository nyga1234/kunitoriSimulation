using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoUI : MonoBehaviour
{
    [SerializeField] InflueneceManager influeneceManager;
    [SerializeField] GameObject mapField;

    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] DialogueUI dialogueUI;

    [SerializeField] Text YesNoText;

    private bool Yes = false;
    private bool isYesNoVisible = false;

    public void ShowEnterUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�d�����܂����H";
    }

    public void ShowCharacterSelectUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "���̃L�����N�^�[�Ńv���C���܂����H";
    }

    public void ShowSearchYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�o�p���܂����H";
    }

    public void ShowEmployedYesNoUI(CharacterController lordCharacter)
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = lordCharacter.characterModel.name + "�R��������˗��ł��B�������܂����H";
    }

    public void ShowBanishmentYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�Ǖ����܂����H";
    }

    public void ShowVagabondYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "���͂�����܂����H";
    }

    public void ShowAttackYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�N�U���܂����H";
    }

    public void ShowBattleCharacterSelectYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "��낵���ł����H";
    }

    public void ShowAbandonYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�퓬��������܂����H";
    }

    public void ShowEndYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�I�����܂����H";
    }

    public void YesButton()
    {
        isYesNoVisible = false;
        this.gameObject.SetActive(false);
        Yes = true;
    }

    public void NoButtone()
    {
        isYesNoVisible = false;
        this.gameObject.SetActive(false);
        Yes = false;
    }

    public bool IsYes()
    {
        return Yes;
    }

    //public void HideDialogueUI()
    //{
    //    // �_�C�A���O���\���ɂ��鏈��
    //    this.gameObject.SetActive(false);
    //    isDialogueVisible = false;
    //}

    public bool IsYesNoVisible()
    {
        return isYesNoVisible;
    }
}
