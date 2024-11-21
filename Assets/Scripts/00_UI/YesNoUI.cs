using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YesNoUI : MonoBehaviour
{
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] GameObject mapField;

    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] DialogueUI dialogueUI;

    [SerializeField] TextMeshProUGUI YesNoText;

    private bool Yes = false;
    private bool isYesNoVisible = false;

    //public void ShowEnterUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "�d�����܂����H";
    //}

    //public void ShowCharacterSelectUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "���̃L�����N�^�[�Ńv���C���܂����H";
    //}

    //public void ShowSearchYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "�o�p���܂����H";
    //}

    public void ShowEmployedYesNoUI(CharacterController lordCharacter)
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = lordCharacter.name + "�R��������˗��ł��B�������܂����H";
    }

    //public void ShowBanishmentYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "�Ǖ����܂����H";
    //}

    //public void ShowVagabondYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "���͂�����܂����H";
    //}

    public void ShowAttackYesNoUI()
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�N�U���܂����H";
    }

    //public void ShowBattleCharacterSelectYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "��낵���ł����H";
    //}

    public void ShowAbandonYesNoUI()
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�퓬��������܂����H";
    }

    public void ShowEndYesNoUI()
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "�I�����܂����H";
    }

    public void YesButton()
    {
        SoundManager.instance.PlayYesSE();
        isYesNoVisible = false;
        this.gameObject.SetActive(false);
        Yes = true;
    }

    public void NoButtone()
    {
        SoundManager.instance.PlayCancelSE();
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
