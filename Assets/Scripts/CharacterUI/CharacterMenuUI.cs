using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening.Core.Easing;

public class CharacterMenuUI : MonoBehaviour
{
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI rankText;

    [SerializeField] TextMeshProUGUI searchText;
    [SerializeField] TextMeshProUGUI banishmentText;

    [SerializeField] Image lordImage;
    [SerializeField] Sprite charaUI;
    [SerializeField] Image character1Image;
    [SerializeField] Image character2Image;
    [SerializeField] Image character3Image;
    [SerializeField] Image character4Image;
    [SerializeField] Image character5Image;
    [SerializeField] Image character6Image;

    [SerializeField] TextMeshProUGUI charaNameText;
    [SerializeField] TextMeshProUGUI territorySumText;
    [SerializeField] TextMeshProUGUI goldSumText;
    [SerializeField] TextMeshProUGUI forceSumText;
    [SerializeField] TextMeshProUGUI soliderSumText;

    [SerializeField]
    private CommandOnClick commandClick;

    [SerializeField]
    private SelectButtonView informationButton;
    [SerializeField]
    private SelectButtonView appointmentButton;
    [SerializeField]
    private SelectButtonView searchButton;
    [SerializeField]
    private SelectButtonView banishmentButton;
    [SerializeField]
    private SelectButtonView functionButton;
    [SerializeField]
    private SelectButtonView endButton;

    private void Start()
    {
        informationButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickInformation());
        appointmentButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickAppointment());
        searchButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickSearch());
        banishmentButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickBanishment());
        functionButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickFunction());
        endButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickLordEnd());
    }

    public void ShowCharacterMenuUI(CharacterController character, Influence influence)
    {
        gameObject.SetActive(true);

        Initialize();

        moneyText.text = "���� " + character.gold.ToString();
        rankText.text = character.rank.ToString();
        
        SearchTextChange();
        BanishmentTextChange();

        lordImage.sprite = character.icon;//�̎�摜�̐ݒ�

        List<CharacterController> noPlayerCharacterList = character.influence.characterList.FindAll(x => !x.isPlayerCharacter);
        for (int i = 0; i < noPlayerCharacterList.Count; i++)
        {
            if (noPlayerCharacterList[i] != null)
            {
                Image characterImage = GetCharacterImage(i + 1);// �e�L�����N�^�[�ɑΉ�����Image�ϐ����擾
                characterImage.sprite = noPlayerCharacterList[i].icon;// �Ή�����Image�ɃX�v���C�g����
            }
        }

        territoryUIOnMouse.InfluenceCalcSum(influence);
        charaNameText.text = character.name;
        territorySumText.text = "[�̒n��] " + influence.territorySum.ToString();
        goldSumText.text = "[�����v]" + influence.goldSum.ToString();
        forceSumText.text = "[�����] " + influence.forceSum.ToString();
        soliderSumText.text = "[���m��] " + influence.soliderSum.ToString();
    }

    private void Initialize()
    {
        searchText.color = Color.white; // ���F�ɕύX
        banishmentText.color = Color.white; // ���F�ɕύX
        InitializeImage();//�̎�z���摜�̏����ݒ�
    }

    void InitializeImage()
    {
        for (int i = 1; i <= 6; i++)
        {
            Image characterImage = GetCharacterImage(i);
            characterImage.sprite = charaUI;
        }
    }

    private void SearchTextChange()
    {
        //�̒n���ɉ������T�������̔�\���ݒ�
        switch (GameMain.instance.playerCharacter.influence.territoryList.Count)
        {
            case 1:
            case 2:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 3 || GameMain.instance.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 4 || GameMain.instance.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
            case 7:
            case 8:
            case 9:
            case 10:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 5 || GameMain.instance.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
            case 11:
            case 12:
            case 13:
            case 14:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 6 || GameMain.instance.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
        }
    }

    private void BanishmentTextChange()
    {
        if (GameMain.instance.playerCharacter.influence.characterList.Count == 1)
        {
            banishmentText.color = new Color32(122, 122, 122, 255);
        }
    }

    // �C���f�b�N�X�ɉ�����Image�ϐ����擾���郁�\�b�h
    private Image GetCharacterImage(int index)
    {
        switch (index)
        {
            case 1: return character1Image;
            case 2: return character2Image;
            case 3: return character3Image;
            case 4: return character4Image;
            case 5: return character5Image;
            case 6: return character6Image;
            default: return null;
        }
    }

    public void HideCharacterMenuUI()
    {
        gameObject.SetActive(false);
    }
}
