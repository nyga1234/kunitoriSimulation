using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuUI : MonoBehaviour
{
    //[SerializeField] InflueneceManager influeneceManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] BattleMenuCommandUI battleMenuCommandUI;

    [SerializeField] Text moneyText;

    [SerializeField] Image lordImage;

    [SerializeField] Sprite charaUI;

    [SerializeField] Image character1Image;
    [SerializeField] Image character2Image;
    [SerializeField] Image character3Image;
    [SerializeField] Image character4Image;
    [SerializeField] Image character5Image;
    [SerializeField] Image character6Image;

    [SerializeField] Text rankText;
    [SerializeField] Text charaNameText;
    [SerializeField] Text territorySumText;
    [SerializeField] Text goldSumText;
    [SerializeField] Text forceSumText;
    [SerializeField] Text soliderSumText;

    public void ShowBattleMenuUI(CharacterController character, Influence influence)
    {
        gameObject.SetActive(true);
        battleMenuCommandUI.ShowBattleMenuCommandUI();

        moneyText.text = "���� " + character.characterModel.gold.ToString();

        if (influence != GameManager.instance.noneInfluence)
        {
            //�̎�摜�̐ݒ�
            CharacterController LordCharacter = character.influence.characterList.Find(chara => chara.characterModel.isLord);
            lordImage.sprite = LordCharacter.characterModel.icon;

            //�̎�z���摜�̐ݒ�
            List<CharacterController> noLordCharacterList = character.influence.characterList.FindAll(x => !x.characterModel.isLord);
            for (int i = 0; i < noLordCharacterList.Count; i++)
            {
                if (noLordCharacterList[i] != null)
                {
                    // �e�L�����N�^�[�ɑΉ�����Image�ϐ����擾
                    Image characterImage = GetCharacterImage(i + 1);

                    // �Ή�����Image�ɃX�v���C�g����
                    characterImage.sprite = noLordCharacterList[i].characterModel.icon;
                }
            }
        }
        else
        {
            lordImage.sprite = character.characterModel.icon;

            character1Image.gameObject.SetActive(false);
            character2Image.gameObject.SetActive(false);
            character3Image.gameObject.SetActive(false);
            character4Image.gameObject.SetActive(false);
            character5Image.gameObject.SetActive(false);
            character6Image.gameObject.SetActive(false);
        }
        
        charaNameText.text = character.characterModel.name;
        rankText.text = character.characterModel.rank.ToString();

        territoryUIOnMouse.InfluenceCalcSum(influence);
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

    public void HideBattleMenuUI()
    {
        gameObject.SetActive(false);
    }
}
