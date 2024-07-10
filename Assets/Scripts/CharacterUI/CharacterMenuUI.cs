using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuUI : MonoBehaviour
{
    //[SerializeField] InflueneceManager influeneceManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] CharacterMenuCommandUI characterMenuCommandUI;

    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] TextMeshProUGUI rankText;

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

    public void ShowCharacterMenuUI(CharacterController character, Influence influence)
    {
        gameObject.SetActive(true);
        characterMenuCommandUI.ShowCharacterMenuCommandUI();
        moneyText.text = "���� " + character.characterModel.gold.ToString();

        rankText.text = character.characterModel.rank.ToString();

        //�̎�摜�̐ݒ�
        lordImage.sprite = character.characterModel.icon;
        //�̎�z���摜�̐ݒ�
        for (int i = 1; i <= 6; i++)
        {
            Image characterImage = GetCharacterImage(i);
            characterImage.sprite = charaUI;
        }
        List<CharacterController> noPlayerCharacterList = character.influence.characterList.FindAll(x => !x.characterModel.isPlayerCharacter);
        for (int i = 0; i < noPlayerCharacterList.Count; i++)
        {
            if (noPlayerCharacterList[i] != null)
            {
                // �e�L�����N�^�[�ɑΉ�����Image�ϐ����擾
                Image characterImage = GetCharacterImage(i + 1);

                // �Ή�����Image�ɃX�v���C�g����
                characterImage.sprite = noPlayerCharacterList[i].characterModel.icon;
            }
        }

        territoryUIOnMouse.InfluenceCalcSum(influence);
        charaNameText.text = character.characterModel.name;
        territorySumText.text = "[�̒n��] " + influence.territorySum.ToString();
        goldSumText.text = "[�����v]" + influence.goldSum.ToString();
        forceSumText.text = "[�����] " + influence.forceSum.ToString();
        soliderSumText.text = "[���m��] " + influence.soliderSum.ToString();
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
