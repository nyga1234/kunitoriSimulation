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
    [SerializeField] private CommandOnClick commandClick;

    [System.Serializable]
    private class ImageGroup
    {
        public Image lordImage;
        public Sprite charaUI;
        public Image character1Image;
        public Image character2Image;
        public Image character3Image;
        public Image character4Image;
        public Image character5Image;
        public Image character6Image;
    }

    [System.Serializable]
    private class ButtonGroup
    {
        public SelectButtonView informationButton;
        public SelectButtonView appointmentButton;
        public SelectButtonView searchButton;
        public SelectButtonView banishmentButton;
        public SelectButtonView functionButton;
        public SelectButtonView endButton;
    }

    [System.Serializable]
    private class TextGroup
    {
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI rankText;
        public TextMeshProUGUI searchText;
        public TextMeshProUGUI banishmentText;
        public TextMeshProUGUI charaNameText;
        public TextMeshProUGUI territorySumText;
        public TextMeshProUGUI goldSumText;
        public TextMeshProUGUI forceSumText;
        public TextMeshProUGUI soliderSumText;
    }

    [SerializeField] private ImageGroup images;
    [SerializeField] private ButtonGroup buttons;
    [SerializeField] private TextGroup texts;

    private void Start()
    {
        buttons.informationButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickInformation());
        buttons.appointmentButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickAppointment());
        buttons.searchButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickSearch());
        buttons.banishmentButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickBanishment());
        buttons.functionButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickFunction());
        buttons.endButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickLordEnd());
    }

    public void ShowCharacterMenuUI(CharacterController character, Influence influence)
    {
        gameObject.SetActive(true);

        Initialize();

        texts.moneyText.text = "資金 " + character.gold.ToString();
        texts.rankText.text = character.rank.ToString();
        
        SearchTextChange();
        BanishmentTextChange();

        images.lordImage.sprite = character.icon;//領主画像の設定

        List<CharacterController> noPlayerCharacterList = character.influence.characterList.FindAll(x => !x.isPlayerCharacter);
        for (int i = 0; i < noPlayerCharacterList.Count; i++)
        {
            if (noPlayerCharacterList[i] != null)
            {
                Image characterImage = GetCharacterImage(i + 1);// 各キャラクターに対応するImage変数を取得
                characterImage.sprite = noPlayerCharacterList[i].icon;// 対応するImageにスプライトを代入
            }
        }

        territoryUIOnMouse.InfluenceCalcSum(influence);
        texts.charaNameText.text = character.name;
        texts.territorySumText.text = "[領地数] " + influence.territorySum.ToString();
        texts.goldSumText.text = "[資金計]" + influence.goldSum.ToString();
        texts.forceSumText.text = "[総戦力] " + influence.forceSum.ToString();
        texts.soliderSumText.text = "[兵士数] " + influence.soliderSum.ToString();
    }

    private void Initialize()
    {
        texts.searchText.color = Color.white; // 白色に変更
        texts.banishmentText.color = Color.white; // 白色に変更
        InitializeImage();//領主配下画像の初期設定
    }

    void InitializeImage()
    {
        for (int i = 1; i <= 6; i++)
        {
            Image characterImage = GetCharacterImage(i);
            characterImage.sprite = images.charaUI;
        }
    }

    private void SearchTextChange()
    {
        //領地数に応じた探索文字の非表示設定
        switch (GameMain.instance.playerCharacter.influence.territoryList.Count)
        {
            case 1:
            case 2:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 3 || GameMain.instance.playerCharacter.gold < 9)
                {
                    texts.searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 4 || GameMain.instance.playerCharacter.gold < 9)
                {
                    texts.searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
            case 7:
            case 8:
            case 9:
            case 10:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 5 || GameMain.instance.playerCharacter.gold < 9)
                {
                    texts.searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
            case 11:
            case 12:
            case 13:
            case 14:
                if (GameMain.instance.playerCharacter.influence.characterList.Count >= 6 || GameMain.instance.playerCharacter.gold < 9)
                {
                    texts.searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
        }
    }

    private void BanishmentTextChange()
    {
        if (GameMain.instance.playerCharacter.influence.characterList.Count == 1)
        {
            texts.banishmentText.color = new Color32(122, 122, 122, 255);
        }
    }

    // インデックスに応じたImage変数を取得するメソッド
    private Image GetCharacterImage(int index)
    {
        switch (index)
        {
            case 1: return images.character1Image;
            case 2: return images.character2Image;
            case 3: return images.character3Image;
            case 4: return images.character4Image;
            case 5: return images.character5Image;
            case 6: return images.character6Image;
            default: return null;
        }
    }

    public void HideCharacterMenuUI()
    {
        gameObject.SetActive(false);
    }
}
